using FantasyLeague.ViewModels.Player;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Team
{
    public class TeamPlayersViewModel : TeamViewModel
    {
        public IList<PlayerStatsViewModel> Players { get; set; }
    }
}
