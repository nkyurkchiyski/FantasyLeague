using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Fixture
{
    public class FixtureStatsViewModel : FixtureViewModel
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        
        public IList<ScorePlayerViewModel> Scores { get; set; }
        
    }
}
