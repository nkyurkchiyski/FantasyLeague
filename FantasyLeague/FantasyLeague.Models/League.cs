using FantasyLeague.Models.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class League : BaseEntity
    {
        public League()
        {
            this.Users = new HashSet<UserLeague>();
            this.Invites = new HashSet<Invite>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<UserLeague> Users { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }

    }
}
