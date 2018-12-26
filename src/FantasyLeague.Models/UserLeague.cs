using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class UserLeague : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public Guid LeagueId { get; set; }
        public virtual League League { get; set; }

    }
}