using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class Player : BaseEntity
    {
        private const double MaxPriceValue = 30;

        public Player()
        {
            this.Rosters = new HashSet<RosterPlayer>();
            this.Scores = new HashSet<Score>();
            this.Transfers = new HashSet<Transfer>();
            this.Active = true;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Injured { get; set; }
        
        public PlayerPosition Position { get; set; }

        [Required]
        [Range(minimum: 0, maximum: MaxPriceValue)]
        public double Price { get; set; }

        [Required]
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        [Required]
        public Guid ImageId { get; set; }
        public virtual Image Image { get; set; }
        
        public bool Active { get; set; }

        public virtual ICollection<RosterPlayer> Rosters { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
