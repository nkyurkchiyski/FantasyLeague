using FantasyLeague.ViewModels.Fixture;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Matchday
{
    public class MatchdayEditViewModel:MatchdayViewModel
    {
        public IList<FixtureViewModel> Fixtures { get; set; }
    }
}
