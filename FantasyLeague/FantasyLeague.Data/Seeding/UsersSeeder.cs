using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Seeding.Contracts;
using FantasyLeague.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FantasyLeague.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        private const string AdminName = "admin";
        private const string AdminPassword = "admin123";
        private const string AdminEmail = "admin@gmail.com";

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
                    Email = AdminEmail
                };

                var createResult = userManager
                    .CreateAsync(user, AdminPassword)
                    .GetAwaiter()
                    .GetResult();

                if (!createResult.Succeeded)
                {
                    throw new Exception(ExceptionConstants.CreateUserException);
                }

                var addToRoleResult = userManager
                    .AddToRoleAsync(user, roleName)
                    .GetAwaiter()
                    .GetResult();

                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception(ExceptionConstants.AddUserToRoleException);
                }

            }
        }
    }
}
