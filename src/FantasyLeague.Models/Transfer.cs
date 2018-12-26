using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class Transfer : BaseEntity
    {
        [Required]
        public Guid RosterId { get; set; }
        public virtual Roster Roster { get; set; }

        [Required]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        
        public TransferType TransferType { get; set; }

    }
}
