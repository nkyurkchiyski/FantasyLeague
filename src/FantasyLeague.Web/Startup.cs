using AutoMapper;
using CloudinaryDotNet;
using FantasyLeague.Data;
using FantasyLeague.Data.Repositories;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Services;
using FantasyLeague.Services.Contracts;
using FantasyLeague.Web.Middlewares.MiddlewareExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyLeague.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FantasyLeagueDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

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
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<FantasyLeagueDbContext>();
            
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });


            //SetUp Cloudinary
            var account = new Account(
               Configuration["Authentication:Cloudinary:CloudName"],
               Configuration["Authentication:Cloudinary:APIKey"],
               Configuration["Authentication:Cloudinary:APISecret"]);

            var cloudinary = new Cloudinary(account);

            services.AddSingleton(new Cloudinary(account));

            //ExternalLogin Setup
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
              mc.AddProfile(new MappingProfile()));

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            //Services
            services.AddScoped<ITransfersService, TransfersService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IFixturesService, FixturesService>();
            services.AddScoped<IMatchdaysService, MatchdaysService>();
            services.AddScoped<IRostersService, RostersService>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddRazorPagesOptions(options =>
                {
                    options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSeedDataMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
