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
    public class MatchdaysServiceTests : BaseServiceTests
    {
        private readonly IMatchdaysService matchdaysService;

        public MatchdaysServiceTests()
        {
            this.matchdaysService = this.Provider.GetService<IMatchdaysService>();
        }

        [Fact]
        public void All_WithExistingData_ShouldReturnTheSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var allMatchdays = this.Context.Matchdays.Count();

            var result = this.matchdaysService
                .All<Matchday>().ToList();

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();

            resultCount.ShouldBe(allMatchdays);

            this.TearDown();
        }

        [Fact]
        public void All_WithoutExistingData_ShouldReturnEmpty()
        {
            var result = this.matchdaysService
                .All<Matchday>().ToList();

            result.ShouldBeEmpty();

            this.TearDown();
        }

        [Fact]
        public void GetMatchday_WithValidData_ShouldReturnMatchday()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var matchday = this.Context.Matchdays.First();

            var result = this.matchdaysService
                .GetMatchday<Matchday>(matchday.Id);

            result.ShouldBeSameAs(matchday);

            this.TearDown();
        }

        [Fact]
        public void GetMatchday_WithInvalidData_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.matchdaysService
                .GetMatchday<Matchday>(Guid.NewGuid());

            result.ShouldBeNull();
            this.TearDown();
        }

        [Fact]
        public void GetMatchday_WithEmptyId_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.matchdaysService
                .GetMatchday<Matchday>(Guid.Empty);

            result.ShouldBeNull();
            this.TearDown();
        }

        [Fact]
        public void GetCurrentMatchday_WithExistingData_ShouldReturnMatchday()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var currentMatchday = this.Context
                .Matchdays.FirstOrDefault(x => x.MatchdayStatus == MatchdayStatus.Current);

            var result = this.matchdaysService
                .GetCurrentMatchday<Matchday>();

            result.ShouldNotBeNull();
            result.ShouldBeSameAs(currentMatchday);

            this.TearDown();
        }

        [Fact]
        public void GetCurrentMatchday_WithNoExistingData_ShouldReturnNull()
        {
            var result = this.matchdaysService
                .GetCurrentMatchday<Matchday>();

            result.ShouldBeNull();
            this.TearDown();
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(56)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        [InlineData(35)]
        public void SetCurrentMatchday_WithInvalidWeek_ShouldReturnNull(int week)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.matchdaysService
                .SetCurrentMatchdayAsync(week, TransferWindowStatus.Closed)
                .GetAwaiter()
                .GetResult();

            result.ShouldBeNull();
            this.TearDown();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(30)]
        [InlineData(10)]
        [InlineData(9)]
        [InlineData(34)]
        public void SetCurrentMatchday_WithValidWeek_ShouldReturnMatchday(int week)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.matchdaysService
                .SetCurrentMatchdayAsync(week, TransferWindowStatus.Closed)
                .GetAwaiter()
                .GetResult();

            result.ShouldNotBeNull();
            this.TearDown();
        }
    }
}
