using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;

namespace FantasyLeague.Models
{
    public class Player : BaseEntity
    {
        public Player()
        {
            this.Rosters = new HashSet<RosterPlayer>();
            this.Scores = new HashSet<Score>();
            this.Transfers = new HashSet<Transfer>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Injured { get; set; }

        public PlayerPosition Position { get; set; }

        public double Price { get; set; }

        public Guid TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid ImageId { get; set; }
        public virtual Image Image { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<RosterPlayer> Rosters { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
