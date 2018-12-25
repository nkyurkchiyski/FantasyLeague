using FantasyLeague.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyLeague.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(e => e.Initials)
                   .IsRequired()
                   .HasColumnType("NCHAR(3)");

            builder.HasOne(e => e.TeamImage)
                   .WithOne(i => i.Team)
                   .HasForeignKey<TeamImage>(e => e.TeamId);
        }
    }
}
