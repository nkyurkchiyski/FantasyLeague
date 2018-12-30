using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Fixture
{
    public class FixtureViewModel
    {
        public Guid Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        
        public FixtureStatus Status { get; set; }

        public bool ScoresAdded { get; set; }
    }
}
