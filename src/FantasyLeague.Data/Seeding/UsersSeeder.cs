using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Seeding.Contracts;
using FantasyLeague.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FantasyLeague.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        private const string AdminName = "admin";
        private const string AdminPassword = "admin123";
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminClubName = "AdminClub";

        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            SeedUser(userManager, RoleConstants.AdminRoleName);
        }

        private void SeedUser(UserManager<User> userManager, string roleName)
        {
            var user = userManager
                .FindByNameAsync(AdminName)
                .GetAwaiter()
                .GetResult();

            if (user == null)
            {
                user = new User
                {
                    UserName = AdminName,
                    Email = AdminEmail,
                    ClubName = AdminClubName
                };

                var createResult = userManager
                    .CreateAsync(user, AdminPassword)
                    .GetAwaiter()
                    .GetResult();

                if (!createResult.Succeeded)
                {
                    throw new Exception(string.Join(";", createResult.Errors.Select(x => x.Description)));
                }

                var addToRoleResult = userManager
                    .AddToRoleAsync(user, roleName)
                    .GetAwaiter()
                    .GetResult();

                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception(string.Join(";", createResult.Errors.Select(x => x.Description)));
                }

            }
        }
    }
}
