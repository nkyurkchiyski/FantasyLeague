using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;

namespace FantasyLeague.Models
{
    public class Player : ImageEntity
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
        public Team Team { get; set; }

        public bool Active { get; set; }

        public ICollection<RosterPlayer> Rosters { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
