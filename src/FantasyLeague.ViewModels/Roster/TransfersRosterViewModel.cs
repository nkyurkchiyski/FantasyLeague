using FantasyLeague.ViewModels.Player;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Roster
{
    public class TransfersRosterViewModel
    {
        public IList<PlayerStatsViewModel> AllPlayers { get; set; }

        public IList<PlayerStatsViewModel> RosterPlayers { get; set; }

        public double Budget { get; set; }

        public int TransfersLeft { get; set; }
    }
}
