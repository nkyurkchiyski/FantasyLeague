using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public virtual ICollection<RosterPlayer> Players { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }

    }
}
