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
            IMatchdaysService matchdaysService,
            IMapper mapper) : base(mapper)
        {
            this.rosterRepository = rosterRepository;
            this.scoreRepository = scoreRepository;
            this.playerRepository = playerRepository;
            this.userRepository = userRepository;
            this.matchdaysService = matchdaysService;
        }

        public async Task<IServiceResult> Create(string username, string[] playerIds)
        {
            var result = new ServiceResult { Succeeded = false };

            if (playerIds.Length != GlobalConstants.RosterSize)
            {
                result.Error = ExceptionConstants.InvalidRosterException;

                return result;
            }

            var matchday = this.matchdaysService.GetCurrentMatchday<Matchday>();

            var user = this.userRepository.All().FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    RoleConstants.UserRoleName);

                return result;
            }

            var players = new List<RosterPlayer>();

            double budget = GlobalConstants.Budget;

            foreach (var id in playerIds)
            {
                var player = await this.playerRepository.GetByIdAsync(new Guid(id));

                if (player == null)
                {
                    result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.PlayerName);

                    return result;
                }

                budget -= player.Price;

                var pr = new RosterPlayer { Player = player };

                players.Add(pr);
            }

            var roster = new Roster
            {
                Formation = Formation.Formation352,
                User = user,
                Budget = budget,
                Players = players,
                Matchday = matchday
            };

            this.rosterRepository.Add(roster);

            await this.rosterRepository.SaveChangesAsync();

            result.Succeeded = true;

            return result;
        }

        public Task<IServiceResult> Edit(Guid rosterId, ICollection<RosterPlayerViewModel> players)
        {
            throw new NotImplementedException();
        }

        public ICollection<RosterViewModel> GetAllUserRosters(string username)
        {
            var rosters = this.rosterRepository.All()
              .Where(x => x.User.UserName == username).ToList();

            var models = rosters.Select(x => this.mapper.Map<RosterViewModel>(x))
               .ToList();

            if (models != null)
            {
                foreach (var rosterVM in models)
                {
                    foreach (var playerVM in rosterVM.Players)
                    {
                        var playerScore = this.scoreRepository.All()
                                        .FirstOrDefault(x => x.Fixture.MatchdayId == rosterVM.Matchday.Id &&
                                                             x.PlayerId == playerVM.Id);

                        if (playerScore != null)
                        {
                            playerVM.CurrentPoints = playerScore.GetScore();
                        }
                    }

                    rosterVM.Points = rosterVM.Players.Sum(x => x.CurrentPoints);
                }

            }
            return models;
        }

        public RosterViewModel GetCurrentUserRoster(string username, Guid matchdayId)
        {
            var roster = this.rosterRepository.All()
                .FirstOrDefault(x => x.User.UserName == username &&
                                   x.MatchdayId == matchdayId);

            var model = this.mapper.Map<RosterViewModel>(roster);

            if (model != null)
            {
                foreach (var playerVM in model.Players)
                {
                    var playerScore = this.scoreRepository.All()
                                        .FirstOrDefault(x => x.Fixture.MatchdayId == matchdayId &&
                                                             x.PlayerId == playerVM.Id);

                    if (playerScore != null)
                    {
                        playerVM.CurrentPoints = playerScore.GetScore();
                    }
                }

                model.Points = model.Players.Sum(x => x.CurrentPoints);
            }

            return model;
        }

        public async Task<IServiceResult> SetCurrentRosters(Guid matchdayId)
        {
            var result = new ServiceResult { Succeeded = false };

            var users = this.userRepository.All().ToList();

            var rosters = new List<Roster>();
            foreach (var user in users)
            {
                var existingRoster = user.Rosters.FirstOrDefault(x => x.MatchdayId == matchdayId);
                if (existingRoster != null)
                {
                    continue;
                }

                var lastRoster = this.rosterRepository.All()
                    .Where(x => x.UserId == user.Id).LastOrDefault();

                if (lastRoster != null)
                {
                    var roster = new Roster
                    {
                        Budget = lastRoster.Budget,
                        MatchdayId = matchdayId,
                        Formation = lastRoster.Formation,
                        User = user
                    };

                    foreach (var player in lastRoster.Players)
                    {
                        roster.Players.Add(new RosterPlayer { PlayerId = player.PlayerId });
                    }

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
