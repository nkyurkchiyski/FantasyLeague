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
            this.Active = true;
            this.Price = 0;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }

        public PlayerPosition Position { get; set; }

        [Required]
        [Range(minimum: 0, maximum: MaxPriceValue)]
        public double Price { get; set; }

        [Required]
        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<RosterPlayer> Rosters { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
