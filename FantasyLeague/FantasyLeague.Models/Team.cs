using FantasyLeague.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class Team : ImageEntity
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Fans = new HashSet<User>();
            this.Fixtures = new HashSet<Fixture>();
        }

        public string Name { get; set; }

        public bool Active { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<User> Fans { get; set; }
        public ICollection<Fixture> Fixtures { get; set; }
        
    }
}
