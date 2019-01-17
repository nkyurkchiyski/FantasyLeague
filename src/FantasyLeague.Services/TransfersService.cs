using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class TransfersService : BaseService, ITransfersService
    {
        private readonly IRepository<RosterPlayer> rosterPlayerRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<Roster> rosterRepository;
        private readonly IRepository<User> userRepository;

        public TransfersService(
            IRepository<RosterPlayer> rosterPlayerRepository,
            IRepository<Player> playerRepository,
            IRepository<Roster> rosterRepository,
            IRepository<User> userRepository,
            IMapper mapper) : base(mapper)
        {
            this.rosterPlayerRepository = rosterPlayerRepository;
            this.playerRepository = playerRepository;
            this.rosterRepository = rosterRepository;
            this.userRepository = userRepository;
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
            return playerIds.All(x => !string.IsNullOrEmpty(x)) &&
                   playerIds.All(x => !string.IsNullOrWhiteSpace(x));
        }

        private bool ValidCountPlayerIds(string[] playersIn, string[] playersOut)
        {
            return playersIn.Length == playersOut.Length &&
                   playersIn.Length <= GlobalConstants.MaxTransfers &&
                   playersOut.Length <= GlobalConstants.MaxTransfers;
        }

        private async Task AddRosterPlayers(Roster roster, string[] playersIn)
        {
            foreach (var id in playersIn)
            {
                var player = await this.playerRepository
                    .GetByIdAsync(new Guid(id));

                roster.Budget -= player.Price;

                var rp = new RosterPlayer { Player = player };

                roster.Players.Add(rp);
            }
        }

        private void RemoveRosterPlayers(Roster roster, string[] playersOut)
        {
            var rosterPlayers = roster.Players
                .Where(x => playersOut.Contains(x.PlayerId.ToString()))
                .ToArray();

            double sumPrices = rosterPlayers.Sum(x => x.Player.Price);

            this.rosterPlayerRepository.DeleteRange(rosterPlayers);

            roster.Budget += sumPrices;
        }

        private void UnselectAllPlayers(Roster roster)
        {
            foreach (var player in roster.Players)
            {
                player.Selected = false;
            }
        }

        public async Task<IServiceResult> TransferPlayersAsync(
            string username,
            string[] playersIn,
            string[] playersOut)
        {
            var result = new ServiceResult { Succeeded = false };

            if (!this.ValidPlayerIds(playersIn) ||
                !this.ValidPlayerIds(playersOut) ||
                !this.ValidCountPlayerIds(playersIn, playersOut))
            {
                result.Error = ExceptionConstants.InvalidRosterException;
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

            var roster = this.rosterRepository.All()
                .FirstOrDefault(x => x.Matchday.MatchdayStatus == MatchdayStatus.Current &&
                                     x.User == user);

            if (roster == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.RosterName);
                return result;
            }

            bool playersExist = await this.PlayersExist(playersIn);

            if (!playersExist)
            {
                result.Error = string.Format(
                ExceptionConstants.NotFoundException,
                GlobalConstants.PlayerName);

                return result;
            }

            this.RemoveRosterPlayers(roster, playersOut);
            await this.AddRosterPlayers(roster, playersIn);
            this.UnselectAllPlayers(roster);

            roster.TransfersLeft -= playersOut.Length;

            await this.rosterRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }

        public TransfersRosterViewModel GetTransfersRosterViewModel(string username)
        {
            var model = new TransfersRosterViewModel();

            var user = this.userRepository.All()
                .FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return null;
            }

            var roster = this.rosterRepository.All()
                .FirstOrDefault(x => x.Matchday.MatchdayStatus == MatchdayStatus.Current &&
                                     x.User == user);

            if (roster == null)
            {
                return null;
            }

            model.Budget = roster.Budget;
            model.TransfersLeft = roster.TransfersLeft;

            var rosterPlayers = roster.Players.Select(x => x.Player).ToList();

            var allPlayers = this.playerRepository.All()
                .Except(rosterPlayers).ToList();

            var allPlayersModels = allPlayers
                .Select(x => this.mapper.Map<PlayerStatsViewModel>(x)).ToList();

            var rosterPlayersModels = rosterPlayers
                .Select(x => this.mapper.Map<PlayerStatsViewModel>(x)).ToList();

            model.AllPlayers = allPlayersModels;
            model.RosterPlayers = rosterPlayersModels;

            return model;
        }
    }
}
