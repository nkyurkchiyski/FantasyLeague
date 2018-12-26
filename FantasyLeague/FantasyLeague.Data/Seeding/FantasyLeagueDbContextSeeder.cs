using FantasyLeague.Data.Seeding.Contracts;
using System;
using System.Collections.Generic;

namespace FantasyLeague.Data.Seeding
{
    public static class FantasyLeagueDbContextSeeder
    {
        public static void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new UsersSeeder(),
                              new TeamsSeeder(),
                              new PlayersSeeder(),
                              new MatchdaysSeeder()
                          };

            foreach (var seeder in seeders)
            {
                seeder.Seed(context, serviceProvider);
                context.SaveChanges();
            }
        }
    }
}
