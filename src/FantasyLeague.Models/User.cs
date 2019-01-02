using FantasyLeague.Models.Enums;
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
            this.Rosters = new HashSet<Roster>();
        }

        public bool Active { get; set; }

        [Required]
        public string ClubName { get; set; }

        public Guid? FavouriteTeamId { get; set; }
        public virtual Team FavouriteTeam { get; set; }

        public virtual ICollection<Roster> Rosters { get; set; }

        [NotMapped]
        public int CurrentPoints => this.Rosters
            .FirstOrDefault(x => x.Matchday.MatchdayStatus == MatchdayStatus.Current &&
                                 x.IsValid)
            .Points;

        [NotMapped]
        public int TotalPoints => this.Rosters
            .Where(x => (x.Matchday.MatchdayStatus == MatchdayStatus.Current ||
                         x.Matchday.MatchdayStatus == MatchdayStatus.Past) &&
                         x.IsValid)
            .Sum(x => x.Points);

    }
}
