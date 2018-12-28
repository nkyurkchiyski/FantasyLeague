using FantasyLeague.ViewModels.Fixture;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Matchday
{
    public class MatchdayDetailsViewModel : MatchdayViewModel
    {
        public ICollection<FixtureViewModel> Fixtures { get; set; }
    }
}
