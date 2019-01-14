using FantasyLeague.ViewModels.Score;
using FantasyLeague.ViewModels.Team;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerPointsViewModel : PlayerStatsViewModel
    {
        public IList<ScorePointsViewModel> Scores { get; set; }

        public TeamImageViewModel Team { get; set; }
    }
}
