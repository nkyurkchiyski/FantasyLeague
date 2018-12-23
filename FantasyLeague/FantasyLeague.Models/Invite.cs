using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class Invite : BaseEntity
    {
        [Required]
        public string InviterId { get; set; }
        public virtual User Inviter { get; set; }

        [Required]
        public string InviteeId { get; set; }
        public virtual User Invitee { get; set; }

        [Required]
        public Guid LeagueId { get; set; }
        public virtual League League { get; set; }

        public InvitationStatus Status { get; set; }

    }
}
