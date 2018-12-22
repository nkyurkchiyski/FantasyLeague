using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public bool Active { get; set; }

        public string ClubName { get; set; }

        public Guid FavouriteTeamId { get; set; }
        public Team FavouriteTeam { get; set; }

        public ICollection<Roster> Rosters { get; set; }
        public ICollection<League> CreatedLeagues { get; set; }
        public ICollection<UserLeague> AllLeagues { get; set; }


    }
}
