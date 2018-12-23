using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FantasyLeague.Models
{
    public class Fixture : BaseEntity
    {
        public Fixture()
        {
            this.Status = FixtureStatus.Upcoming;
            this.Scores = new HashSet<Score>();
        }

        [Required]
        public Guid HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        [Required]
        public Guid AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }

        public int? HomeTeamGoals { get; set; }

        public int? AwayTeamGoals { get; set; }

        public MatchResult Result { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public FixtureStatus Status { get; set; }

        [Required]
        public Guid MatchdayId { get; set; }
        public virtual Matchday Matchday { get; set; }

        public virtual ICollection<Score> Scores { get; set; }
    }
}
