using System;

namespace FantasyLeague.Data.Seeding.Contracts
{
    public interface ISeeder
    {
        void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider);
    }
}
