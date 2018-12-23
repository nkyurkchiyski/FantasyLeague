using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class UserLeague : BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public Guid LeagueId { get; set; }
        public virtual League League { get; set; }

    }
}