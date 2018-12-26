using FantasyLeague.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Data.Configurations
{
    public class InviteConfiguration : IEntityTypeConfiguration<Invite>
    {
        public void Configure(EntityTypeBuilder<Invite> builder)
        {
            builder.HasOne(e => e.Inviter)
                   .WithMany(i => i.SentInvitations)
                   .HasForeignKey(e => e.InviterId);

            builder.HasOne(e => e.Invitee)
                   .WithMany(i => i.ReceivedInvitations)
                   .HasForeignKey(e => e.InviteeId);
        }
    }
}
