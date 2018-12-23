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

        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<UserLeague> Users { get; set; }
    }
}
