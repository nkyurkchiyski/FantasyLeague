using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class UserLeague : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public Guid LeagueId { get; set; }
        public League League { get; set; }

    }
}