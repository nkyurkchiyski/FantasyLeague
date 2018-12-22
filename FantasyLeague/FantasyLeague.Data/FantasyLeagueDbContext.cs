using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
