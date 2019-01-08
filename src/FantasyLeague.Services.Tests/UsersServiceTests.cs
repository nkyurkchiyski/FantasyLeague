using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Linq;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class UsersServiceTests : BaseServiceTests
    {
        private readonly IUsersService usersService;

        public UsersServiceTests()
        {
            this.usersService = this.Provider.GetService<IUsersService>();
        }

        [Theory]
        [InlineData("Some Name")]
        [InlineData("My Club")]
        [InlineData(" ")]
        public void ClubNameVacant_WithNonExistingData_ShouldReturnTrue(string clubName)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.usersService.ClubNameVacant(clubName);

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void ClubNameVacant_WithExistingData_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService.ClubNameVacant(user.ClubName);

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void GetClubNameAsync_WithExistingData_ShouldReturnString()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService
                .GetClubNameAsync(user.Id)
                .GetAwaiter()
                .GetResult();

            result.ShouldBe(user.ClubName);
            this.TearDown();
        }

        [Theory]
        [InlineData("SomeId")]
        [InlineData("ID")]
        [InlineData("123546")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void GetClubNameAsync_WithNonExistingData_ShouldReturnNull(string userId)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.usersService
                .GetClubNameAsync(userId)
                .GetAwaiter()
                .GetResult();

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void GetFavouriteTeamAsync_WithExistingData_ShouldReturnString()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService
                .GetFavouriteTeamAsync(user.Id)
                .GetAwaiter()
                .GetResult();

            result.ShouldBe(user.FavouriteTeam);
            this.TearDown();
        }

        [Theory]
        [InlineData("SomeId")]
        [InlineData("ID")]
        [InlineData("123546")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void GetFavouriteTeamAsync_WithNonExistingData_ShouldReturnNull(string userId)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.usersService
                .GetFavouriteTeamAsync(userId)
                .GetAwaiter()
                .GetResult();

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void SetFavouriteTeamAsync_WithExistingData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var user = this.Context.Users.First();

            var result = this.usersService
                .SetFavouriteTeamAsync(user.Id, team.Name)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void SetFavouriteTeamAsync_WithNullTeamName_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService
                .SetFavouriteTeamAsync(user.Id, null)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Theory]
        [InlineData("Some Name")]
        [InlineData("Bundesliga Team")]
        [InlineData("BaYeRn MuNcHeN")]
        [InlineData("123546")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void SetFavouriteTeamAsync_WithInvalidTeamName_ShouldReturnFalse(string teamName)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService
                .SetFavouriteTeamAsync(user.Id, teamName)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Theory]
        [InlineData("SomeId")]
        [InlineData("123546")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void SetFavouriteTeamAsync_WithInvalidUserId_ShouldReturnFalse(string userId)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var result = this.usersService
                .SetFavouriteTeamAsync(userId, team.Name)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Theory]
        [InlineData("username")]
        [InlineData("123546")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        public void GetUserByUsername_WithInvalidUsername_ShouldReturnNull(string username)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.usersService
                .GetUserByUsername<User>(username);

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void GetUserByUsername_WithValidUsername_ShouldReturnUser()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();

            var result = this.usersService
                .GetUserByUsername<User>(user.UserName);

            result.ShouldBeSameAs(user);

            this.TearDown();
        }

        [Fact]
        public void GetLeaderboard_WithExistingData_ShouldReturnTheSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var users = this.Context.Users.ToList();

            var result = this.usersService
                .GetLeaderboard<User>();

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();

            resultCount.ShouldBe(users.Count);

            this.TearDown();
        }

        [Fact]
        public void GetLeaderboard_WithExistingData_ShouldReturnEmpty()
        {
            var result = this.usersService
                .GetLeaderboard<User>();
            
            result.ShouldBeEmpty();
        }
    }
}
