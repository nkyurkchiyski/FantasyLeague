using FantasyLeague.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class League : BaseEntity
    {
        public League()
        {
            this.Users = new HashSet<UserLeague>();
        }
        
        public string Name { get; set; }

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }

        public ICollection<UserLeague> Users { get; set; }
    }
}
