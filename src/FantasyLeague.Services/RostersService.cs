using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class RostersService : BaseService, IRostersService
    {
        private readonly IRepository<RosterPlayer> rosterPlayerRepository;
        private readonly IRepository<Roster> rosterRepository;
        private readonly IRepository<Score> scoreRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<User> userRepository;
        private readonly IMatchdaysService matchdaysService;

        public RostersService(
            IRepository<Roster> rosterRepository,
            IRepository<Score> scoreRepository,
            IRepository<Player> playerRepository,
            IRepository<User> userRepository,
            IRepository<RosterPlayer> rosterPlayerRepository,
            IMatchdaysService matchdaysService,
            IMapper mapper) : base(mapper)
        {
            this.rosterRepository = rosterRepository;
            this.scoreRepository = scoreRepository;
            this.playerRepository = playerRepository;
            this.userRepository = userRepository;
            this.matchdaysService = matchdaysService;
            this.rosterPlayerRepository = rosterPlayerRepository;
        }

        private async Task<Roster> CreateRoster(
           Guid matchdayId,
           string userId,
           string[] playerIds)
        {
            var players = new List<RosterPlayer>();

            double budget = GlobalConstants.Budget;

            foreach (var id in playerIds)
            {
                var player = await this.playerRepository
                    .GetByIdAsync(new Guid(id));

                budget -= player.Price;

                var pr = new RosterPlayer { Player = player };

                players.Add(pr);
            }

            return new Roster
            {
                Formation = Formation.Formation442,
                UserId = userId,
                Budget = budget,
                Players = players,
                MatchdayId = matchdayId
            };
        }

        private Roster CopyOldRoster(Matchday matchday, User user, Roster lastRoster)
        {
            var roster = new Roster
            {
                Budget = GlobalConstants.Budget - lastRoster.Players.Sum(x => x.Player.Price),
                MatchdayId = matchday.Id,
                Formation = lastRoster.Formation,
                User = user
            };

            foreach (var player in lastRoster.Players)
            {
                roster.Players.Add(new RosterPlayer
                {
                    PlayerId = player.PlayerId,
                    Selected = player.Selected
                });
            }

            return roster;
        }

        private void CreateRosterViewModel(Guid matchdayId, RosterViewModel model)
        {
            foreach (var playerVM in model.Players)
            {
                var playerScore = this.scoreRepository.All()
                                    .FirstOrDefault(x => x.Fixture.MatchdayId == matchdayId &&
                                                         x.PlayerId == playerVM.PlayerId);

                if (playerScore != null)
                {
                    playerVM.CurrentPoints = playerScore.GetScore();

                    if (playerVM.Selected)
                    {
                        model.Points += playerVM.CurrentPoints;
                    }
                }
            }
        }

        private async Task<bool> PlayersExist(string[] playerIds)
        {
            foreach (var id in playerIds)
            {
                var player = await this.playerRepository
                    .GetByIdAsync(new Guid(id));

                if (player == null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidPlayerIds(string[] playerIds)
        {
            return playerIds.Length == GlobalConstants.RosterSize &&
                   playerIds.All(x => !string.IsNullOrEmpty(x)) &&
                   playerIds.All(x => !string.IsNullOrWhiteSpace(x));
        }
        
        public async Task<IServiceResult> CreateAsync(string username, string[] playerIds)
        {
            var result = new ServiceResult { Succeeded = false };

            if (!this.ValidPlayerIds(playerIds))
            {
                result.Error = ExceptionConstants.InvalidRosterException;
                return result;
            }

            var matchday = this.matchdaysService.GetCurrentMatchday<Matchday>();

            if (matchday == null)
            {
                result.Error = string.Format(
                   ExceptionConstants.NotFoundException,
                   GlobalConstants.MatchdayName);
                return result;
            }

            var user = this.userRepository.All()
                .FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    RoleConstants.UserRoleName);
                return result;
            }

            var previousRoster = user.Rosters
                .FirstOrDefault(x => x.MatchdayId == matchday.Id);

            if (previousRoster != null)
            {
                user.Rosters.Remove(previousRoster);
            }

            bool playersExist = await this.PlayersExist(playerIds);

            if (!playersExist)
            {
                result.Error = string.Format(
                ExceptionConstants.NotFoundException,
                GlobalConstants.PlayerName);

                return result;
            }

            Roster roster = await this.CreateRoster(matchday.Id, user.Id, playerIds);
            this.rosterRepository.Add(roster);
            await this.rosterRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> EditAsync(ICollection<RosterPlayerViewModel> players)
        {
            var result = new ServiceResult { Succeeded = false };

            if (players.Count != GlobalConstants.RosterSize)
            {
                result.Error = string.Format(ExceptionConstants.InvalidRosterException);
                return result;
            }

            foreach (var rp in players)
            {
                var rosterPlayer = await this.rosterPlayerRepository
                    .GetByIdAsync(rp.Id);

                if (rosterPlayer == null)
                {
                    result.Error = string.Format(
                        ExceptionConstants.NotFoundException,
                        GlobalConstants.PlayerName);
                    return result;
                }
                rosterPlayer.Selected = rp.Selected;
            }

            await this.rosterPlayerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public async Task<IServiceResult> SetNewFormationAsync(Formation formation, Guid rosterId)
        {
            var result = new ServiceResult { Succeeded = false };

            var roster = await this.rosterRepository
                .GetByIdAsync(rosterId);

            if (roster == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.RosterName);
                return result;
            }

            roster.Formation = formation;

            await this.rosterPlayerRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public ICollection<RosterViewModel> GetAllUserRosters(string username)
        {
            var rosters = this.rosterRepository.All()
              .Where(x => x.User.UserName == username).ToList();

            var models = rosters.Select(x => this.mapper.Map<RosterViewModel>(x))
               .ToList();

            if (models != null && models.Any())
            {
                foreach (var rosterVM in models)
                {
                    this.CreateRosterViewModel(rosterVM.Matchday.Id, rosterVM);
                }

            }
            return models;
        }

        public RosterViewModel GetCurrentUserRoster(string username)
        {
            var currentMatchday = this.matchdaysService
                .GetCurrentMatchday<Matchday>();

            if (currentMatchday == null)
            {
                return null;
            }

            var roster = this.rosterRepository.All()
                .FirstOrDefault(x => x.User.UserName == username &&
                                   x.MatchdayId == currentMatchday.Id);

            var model = this.mapper.Map<RosterViewModel>(roster);

            if (model != null)
            {
                this.CreateRosterViewModel(currentMatchday.Id, model);
            }
            return model;
        }

        public async Task<IServiceResult> SetCurrentRostersAsync(Guid matchdayId)
        {
            var result = new ServiceResult { Succeeded = false };

            if (matchdayId == Guid.Empty)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            var matchday = this.matchdaysService
                .GetMatchday<Matchday>(matchdayId);

            if (matchday == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.MatchdayName);
                return result;
            }

            var users = this.userRepository.All().ToList();

            var rosters = new List<Roster>();
            foreach (var user in users)
            {
                var existingRoster = user.Rosters
                    .FirstOrDefault(x => x.MatchdayId == matchdayId);

                if (existingRoster != null)
                {
                    continue;
                }

                var lastRoster = this.rosterRepository.All()
                    .Where(x => x.UserId == user.Id)
                    .OrderBy(x => x.Matchday.Week)
                    .LastOrDefault();

                if (lastRoster != null)
                {
                    Roster roster = this.CopyOldRoster(matchday, user, lastRoster);

                    rosters.Add(roster);
                }
            }
            this.rosterRepository.AddRange(rosters.ToArray());

            await this.rosterRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

    }
}
