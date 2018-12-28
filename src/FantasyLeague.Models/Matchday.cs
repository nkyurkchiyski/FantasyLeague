using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FantasyLeague.Models
{
    public class Matchday : BaseEntity
    {
        public Matchday()
        {
            this.Fixtures = new HashSet<Fixture>();
            this.Rosters = new HashSet<Roster>();

            this.MatchdayStatus = MatchdayStatus.Upcoming;
            this.TransferWindowStatus = TransferWindowStatus.Closed;
        }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public MatchdayStatus MatchdayStatus { get; set; }

        [Required]
        public TransferWindowStatus TransferWindowStatus { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Week { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Roster> Rosters { get; set; }
    }
}
