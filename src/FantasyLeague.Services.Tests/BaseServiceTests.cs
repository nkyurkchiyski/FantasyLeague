using AutoMapper;
using CloudinaryDotNet;
using FantasyLeague.Data;
using FantasyLeague.Data.Repositories;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FantasyLeague.Services.Tests
{
    public abstract class BaseServiceTests
    {
        protected BaseServiceTests()
        {
            Mapper.Reset();
            var services = SetUpServices();
            
            this.Provider = services.BuildServiceProvider();
            this.Context = Provider.GetService<FantasyLeagueDbContext>();
        }

        private IConfiguration Configuration { get; }

        protected IServiceProvider Provider { get; set; }

        protected FantasyLeagueDbContext Context { get; set; }


        public void TearDown()
        {
            this.Context.Database.EnsureDeleted();
        }
        
        private ServiceCollection SetUpServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<FantasyLeagueDbContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid()
                    .ToString()));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IFixturesService, FixturesService>();
            services.AddScoped<IMatchdaysService, MatchdaysService>();
            services.AddScoped<IRostersService, RostersService>();
            
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.SignIn.RequireConfirmedEmail = false;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<FantasyLeagueDbContext>();
            
            var mappingConfig = new MapperConfiguration(mc =>
               mc.AddProfile(new MappingProfile())
           );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            return services;
        }
    }
}

