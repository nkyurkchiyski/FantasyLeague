using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Seeding.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FantasyLeague.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {
        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            SeedRole(roleManager, RoleConstants.AdminRoleName);
            SeedRole(roleManager, RoleConstants.UserRoleName);
        }

        private void SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = roleManager
                .FindByNameAsync(roleName)
                .GetAwaiter()
                .GetResult();

            if (role == null)
            {
                var result = roleManager
                    .CreateAsync(new IdentityRole(roleName))
                    .GetAwaiter()
                    .GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(ExceptionConstants.CreateRoleException);
                }
            }
        }
    }
}
