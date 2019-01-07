using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class FixturesServiceTests : BaseServiceTests
    {
        private readonly IFixturesService fixturesService;

        public FixturesServiceTests()
        {
            this.fixturesService = this.Provider.GetService<IFixturesService>();
        }

        [Fact]
        public void All_WithExistingData_ShouldReturnTheSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var allFixtures = this.Context.Fixtures.Count();

            var result = this.fixturesService
                .All<Fixture>().ToList();

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();

            resultCount.ShouldBe(allFixtures);

            this.TearDown();
        }

        [Fact]
        public void All_WithoutExistingData_ShouldReturnEmpty()
        {
            var result = this.fixturesService
                .All<Fixture>().ToList();

            result.ShouldBeEmpty();

            this.TearDown();
        }

        [Fact]
        public void GetFixture_WithValidData_ShouldReturnFixture()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var fixture = this.Context.Fixtures.First();

            var result = this.fixturesService
                .GetFixture<Fixture>(fixture.Id);

            result.ShouldBeSameAs(fixture);

            this.TearDown();
        }

        [Fact]
        public void GetFixture_WithInvalidData_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.fixturesService
                .GetFixture<Fixture>(Guid.NewGuid());

            result.ShouldBeNull();

            this.TearDown();
        }

        [Theory]
        [InlineData(-5, -4)]
        [InlineData(5, -4)]
        [InlineData(-5, 4)]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(int.MinValue, 4)]
        [InlineData(4, int.MinValue)]
        public void Edit_WithInvalidGoals_ShouldReturnFalse(
            int homeTeamGoals,
            int awayTeamGoals)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var fixture = this.Context.Fixtures.First();

            var result = this.fixturesService.EditAsync(
                fixture.Id,
                DateTime.Now,
                FixtureStatus.Finished,
                homeTeamGoals,
                awayTeamGoals)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void Edit_WithEmptyFixtureId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.fixturesService
                .EditAsync(Guid.Empty, DateTime.Now, FixtureStatus.Finished, 0, 0)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void Edit_WithInvalidFixtureId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.fixturesService
                .EditAsync(Guid.NewGuid(), DateTime.Now, FixtureStatus.Finished, 0, 0)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void Edit_WithValidData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var fixture = this.Context.Fixtures.First();

            var result = this.fixturesService
                .EditAsync(fixture.Id, DateTime.Now, FixtureStatus.Finished, 0, 0)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();

            this.TearDown();
        }

        [Fact]
        public void GenerateScores_WithValidData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var matchday = this.Context.Matchdays.First();

            var result = this.fixturesService
                .GenerateScoresAsync(matchday.Id)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();

            this.TearDown();
        }

        [Fact]
        public void GenerateScores_WithInvalidMatchdayId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            
            var result = this.fixturesService
                .GenerateScoresAsync(Guid.NewGuid())
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void GenerateScores_WithEmptyMatchdayId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.fixturesService
                .GenerateScoresAsync(Guid.Empty)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void GenerateScores_ForAlreadyGeneratedMatchday_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var matchday = this.Context.Matchdays.First();

            this.fixturesService.GenerateScoresAsync(matchday.Id);

            var result = this.fixturesService
                .GenerateScoresAsync(Guid.Empty)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }
    }
}
