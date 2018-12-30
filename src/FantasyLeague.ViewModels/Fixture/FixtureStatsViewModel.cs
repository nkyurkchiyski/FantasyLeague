using FantasyLeague.ViewModels.Player;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Fixture
{
    public class FixtureStatsViewModel : FixtureViewModel
    {
        public IList<PlayerViewModel> HomePlayers { get; set; }
        public IList<PlayerViewModel> AwayPlayers { get; set; }

        public IList<ScoreViewModel> Scores { get; set; }
    }
}
