using FantasyLeague.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantasyLeague.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasOne(e => e.Team)
                   .WithOne(i => i.TeamImage)
                   .HasForeignKey<Team>(e => e.TeamImageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Player)
                   .WithOne(i => i.PlayerImage)
                   .HasForeignKey<Player>(e => e.PlayerImageId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasDiscriminator<string>("ImageType");
        }
    }
}
