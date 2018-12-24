using FantasyLeague.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyLeague.Data.Configurations
{
    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.HasOne(e => e.HomeTeam)
                   .WithMany(t => t.HomeFixtures)
                   .HasForeignKey(e => e.HomeTeamId);

            builder.HasOne(e => e.AwayTeam)
                   .WithMany(t => t.AwayFixtures)
                   .HasForeignKey(e => e.AwayTeamId);
        }
    }
}
