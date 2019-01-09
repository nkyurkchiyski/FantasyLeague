using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FantasyLeague.Models
{
    public class Roster : BaseEntity
    {
        public Roster()
        {
            this.Players = new HashSet<RosterPlayer>();
            this.Budget = GlobalConstants.Budget;
        }

        public Formation Formation { get; set; }

        public double Budget { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public Guid MatchdayId { get; set; }
        public virtual Matchday Matchday { get; set; }

        public virtual ICollection<RosterPlayer> Players { get; set; }

        public int Points => this.Players
            .Where(x => x.Selected)
            .Sum(x => x.Player.Scores.FirstOrDefault(y => this.Matchday.Fixtures.Contains(y.Fixture)) == null ? 0 :
                      x.Player.Scores.FirstOrDefault(y => this.Matchday.Fixtures.Contains(y.Fixture)).GetScore());

        [NotMapped]
        public bool IsValid => this.Players.Count == GlobalConstants.RosterSize && this.Budget >= 0;
    }
}
