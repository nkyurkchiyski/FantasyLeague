using FantasyLeague.ViewModels.Fixture;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Matchday
{
    public class MatchdayDetailsVieModel:MatchdayViewModel
    {
        public ICollection<FixtureViewModel> Fixtures { get; set; }
    }
}
