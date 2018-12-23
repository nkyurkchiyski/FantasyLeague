using FantasyLeague.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class Matchday : BaseEntity
    {
        public Matchday()
        {
            this.Fixtures = new HashSet<Fixture>();
            this.Transfers = new HashSet<Transfer>();
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Week { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
