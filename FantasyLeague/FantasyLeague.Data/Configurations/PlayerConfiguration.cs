using FantasyLeague.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasOne(e => e.PlayerImage)
                   .WithOne(i => i.Player)
                   .HasForeignKey<PlayerImage>(e => e.PlayerId);

        }
    }
}
