using FantasyLeague.ViewModels.Fixture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Matchday
{
    public class MatchdayViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Week { get; set; }

        public ICollection<FixtureViewModel> Fixtures { get; set; }
    }
}
