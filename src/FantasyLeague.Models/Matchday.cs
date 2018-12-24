using FantasyLeague.Models.Abstract;
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
            this.Transfers = new HashSet<Transfer>();
        }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Week { get; set; }

        public virtual ICollection<Fixture> Fixtures { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
