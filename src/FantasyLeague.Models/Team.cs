using FantasyLeague.Models.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FantasyLeague.Models
{
    public class Team : BaseEntity
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Fans = new HashSet<User>();
            this.HomeFixtures = new HashSet<Fixture>();
            this.AwayFixtures = new HashSet<Fixture>();
            this.Active = true;
        }

        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }

        [Required]
        public string Initials { get; set; }
        
        public Guid? TeamImageId { get; set; }
        public virtual Image TeamImage { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<User> Fans { get; set; }

        public virtual ICollection<Fixture> HomeFixtures { get; set; }
        public virtual ICollection<Fixture> AwayFixtures { get; set; }

    }
}
