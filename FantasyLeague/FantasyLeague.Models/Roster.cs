using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class Roster : BaseEntity
    {
        public Roster()
        {
            this.Players = new HashSet<RosterPlayer>();
            this.Transfers = new HashSet<Transfer>();
        }

        public Formation Formation { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<RosterPlayer> Players { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }

    }
}
