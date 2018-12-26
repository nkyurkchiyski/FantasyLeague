using FantasyLeague.Data;
using FantasyLeague.Data.Seeding;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FantasyLeague.Web.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
        {
            var dbContext = provider.GetRequiredService<FantasyLeagueDbContext>();

            FantasyLeagueDbContextSeeder.Seed(dbContext, provider);

            await this.next(context);
        }
    }
}
