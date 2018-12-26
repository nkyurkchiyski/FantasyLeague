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
            this.Transfers = new HashSet<Transfer>();
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
        public virtual ICollection<Transfer> Transfers { get; set; }

        [NotMapped]
        public int Points => this.Players
                                 .Sum(x =>
                                      x.Player
                                       .Scores
                                       .First(y =>
                                              this.Matchday
                                              .Fixtures
                                              .Contains(y.Fixture))
                                       .GetScore());
    }
}
