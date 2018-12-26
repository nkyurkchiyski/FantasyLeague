using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FantasyLeague.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Active = true;

            this.AllLeagues = new HashSet<UserLeague>();
            this.CreatedLeagues = new HashSet<League>();

            this.Rosters = new HashSet<Roster>();

            this.SentInvitations = new HashSet<Invite>();
            this.ReceivedInvitations = new HashSet<Invite>();
        }

        public bool Active { get; set; }

        [Required]
        public string ClubName { get; set; }

        public Guid? FavouriteTeamId { get; set; }
        public virtual Team FavouriteTeam { get; set; }

        public virtual ICollection<Roster> Rosters { get; set; }
        public virtual ICollection<League> CreatedLeagues { get; set; }
        public virtual ICollection<UserLeague> AllLeagues { get; set; }

        public virtual ICollection<Invite> SentInvitations { get; set; }
        public virtual ICollection<Invite> ReceivedInvitations { get; set; }

        [NotMapped]
        public int TotalPoints => this.Rosters.Sum(x => x.Points);

    }
}
