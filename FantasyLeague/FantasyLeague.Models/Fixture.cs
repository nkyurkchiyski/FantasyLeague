using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class Fixture : BaseEntity
    {
        public Fixture()
        {
            this.Scores = new HashSet<Score>();
        }

        public Guid HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int? HomeTeamGoals { get; set; }

        public Guid AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int? AwayTeamGoals { get; set; }

        public MatchResult Result { get; set; }
        public DateTime Date { get; set; }
        public FixtureStatus Status { get; set; }

        public Guid MatchdayId { get; set; }
        public Matchday Matchday { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}
