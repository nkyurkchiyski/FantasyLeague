using System;

namespace FantasyLeague.Data.Seeding.Dtos
{
    public class FixtureDto
    {
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int? HomeTeamGoals { get; set; }

        public int? AwayTeamGoals { get; set; }

        public string Winner { get; set; }
    }
}
