using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Fixture
{
    public class FixtureViewModel
    {
        [Required]
        public string HomeTeam { get; set; }

        [Required]
        public string AwayTeam { get; set; }

        public int? HomeTeamGoals { get; set; }

        public int? AwayTeamGoals { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
}
