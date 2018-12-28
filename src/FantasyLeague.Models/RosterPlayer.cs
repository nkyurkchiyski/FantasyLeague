using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class RosterPlayer : BaseEntity
    {
        public RosterPlayer()
        {
            this.IsSub = true;
        }

        [Required]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public Guid RosterId { get; set; }
        public virtual Roster Roster { get; set; }

        public bool IsSub { get; set; }
    }
}