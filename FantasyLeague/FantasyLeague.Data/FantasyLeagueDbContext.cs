using System;
using System.Collections.Generic;
using System.Text;
using FantasyLeague.Data.Configurations;
using FantasyLeague.Models;
using FantasyLeague.Models.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FantasyLeague.Data
{
    public class FantasyLeagueDbContext : IdentityDbContext
    {
        public FantasyLeagueDbContext(DbContextOptions<FantasyLeagueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fixture> Fixtures { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<League> Leagues { get; set; }

        public DbSet<UserLeague> UsersLeagues { get; set; }

        public DbSet<Matchday> Matchdays { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Roster> Rosters { get; set; }

        public DbSet<RosterPlayer> RostersPlayers { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FixtureConfiguration());
            builder.ApplyConfiguration(new LeagueConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new InviteConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
