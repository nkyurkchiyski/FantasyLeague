using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class TeamsServiceTests : BaseServiceTests
    {
        private readonly ITeamsService teamsService;

        public TeamsServiceTests()
        {
            this.teamsService = this.Provider.GetService<ITeamsService>();
        }

        [Fact]
        public void All_WithExistingData_ShouldReturnTheSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var allTeams = this.Context.Teams.Count();

            var result = this.teamsService
                .All<Team>().ToList();

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();

            resultCount.ShouldBe(allTeams);

            this.TearDown();
        }

        [Fact]
        public void All_WithoutExistingData_ShouldReturnEmpty()
        {
            var result = this.teamsService
                .All<Team>().ToList();

            result.ShouldBeEmpty();

            this.TearDown();
        }
        
        [Fact]
        public void GetTeamById_WithValidId_ShouldReturnTeam()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var result = this.teamsService
                .GetTeamById<Team>(team.Id);

            result.ShouldBeSameAs(team);

            this.TearDown();
        }

        [Fact]
        public void GetTeamById_WithInvalidId_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            
            var result = this.teamsService
                .GetTeamById<Team>(Guid.NewGuid());

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void GetTeamById_WithEmptyId_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.teamsService
                .GetTeamById<Team>(Guid.Empty);

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void GetTeamByName_WithValidName_ShouldReturnTeam()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var result = this.teamsService
                .GetTeamByName<Team>(team.Name);

            result.ShouldBeSameAs(team);

            this.TearDown();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("SomeTeam")]
        [InlineData("BAYErn MUnich")]
        [InlineData("FCB")]
        public void GetTeamByName_WithInvalidName_ShouldReturnNull(string name)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            
            var result = this.teamsService
                .GetTeamByName<Team>(name);

            result.ShouldBeNull();

            this.TearDown();
        }
    }
}
