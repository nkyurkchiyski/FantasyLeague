using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Roster;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class RostersServiceTests : BaseServiceTests
    {
        private readonly IRostersService rostersService;
        private readonly IMapper mapper;

        public RostersServiceTests()
        {
            this.rostersService = this.Provider.GetService<IRostersService>();
            this.mapper = this.Provider.GetService<IMapper>();
        }

        private ICollection<RosterPlayer> CreateRosterPlayers()
        {
            var rnd = new Random();

            var players = this.Context.Players
               .OrderBy(x => rnd.Next())
               .Take(15).ToList();

            var rosterPlayers = new List<RosterPlayer>();

            foreach (var pl in players)
            {
                rosterPlayers.Add(new RosterPlayer
                {
                    PlayerId = pl.Id,
                    Selected = false
                });
            }

            return rosterPlayers;
        }

        private Roster CreateRoster(User user, Matchday matchday)
        {
            var rosterPlayers = CreateRosterPlayers();

            var roster = new Roster
            {
                User = user,
                Matchday = matchday,
                Players = rosterPlayers,
                Budget = 0
            };

            return roster;
        }

        [Fact]
        public void CreateAsync_WithValidPlayerIds_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var playerIds = this.Context.Players.Take(15)
                .Select(x => x.Id.ToString()).ToArray();

            var result = this.rostersService
                .CreateAsync(user.UserName, playerIds)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();

            this.TearDown();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(18)]
        [InlineData(55)]
        [InlineData(100)]
        [InlineData(20)]
        [InlineData(16)]
        public void CreateAsync_WithInvalidPlayerIdsCount_ShouldReturnFalse(int count)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var playerIds = this.Context.Players.Take(count)
                .Select(x => x.Id.ToString()).ToArray();

            var result = this.rostersService
                .CreateAsync(user.UserName, playerIds)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithInvalidPlayerIds_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var playerIds = new List<string>();

            for (int i = 0; i < 15; i++)
            {
                playerIds.Add(Guid.NewGuid().ToString());
            }

            var result = this.rostersService
                .CreateAsync(user.UserName, playerIds.ToArray())
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithEmptyPlayerIds_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var playerIds = new List<string>();

            for (int i = 0; i < 15; i++)
            {
                playerIds.Add("");
            }

            var result = this.rostersService
                .CreateAsync(user.UserName, playerIds.ToArray())
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithNullPlayerIds_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var playerIds = new List<string>();

            for (int i = 0; i < 15; i++)
            {
                playerIds.Add(null);
            }

            var result = this.rostersService
                .CreateAsync(user.UserName, playerIds.ToArray())
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithValidData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var matchday = this.Context.Matchdays.First();
            var roster = CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var models = roster.Players
                .Select(x => this.mapper.Map<RosterPlayerViewModel>(x))
                .ToList();

            var result = this.rostersService
                .EditAsync(models)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithInvalidData_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var matchday = this.Context.Matchdays.First();
            var roster = CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var models = new List<RosterPlayerViewModel>();

            for (int i = 0; i < GlobalConstants.RosterSize; i++)
            {
                models.Add(new RosterPlayerViewModel
                {
                    Id = Guid.NewGuid()
                });
            }

            var result = this.rostersService
                .EditAsync(models)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithInvalidRosterSize_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var user = this.Context.Users.First();
            var matchday = this.Context.Matchdays.First();
            var roster = CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var models = new List<RosterPlayerViewModel>();

            for (int i = 0; i < 8; i++)
            {
                models.Add(new RosterPlayerViewModel
                {
                    Id = Guid.NewGuid()
                });
            }

            var result = this.rostersService
                .EditAsync(models)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void SetNewFormationAsync_WithValidData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();
            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .SetNewFormationAsync(Formation.Formation442, roster.Id)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void SetNewFormationAsync_WithInvalidRosterId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();
            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .SetNewFormationAsync(Formation.Formation442, Guid.NewGuid())
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void SetNewFormationAsync_WithEmptyRosterId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();
            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .SetNewFormationAsync(Formation.Formation442, Guid.Empty)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void SetCurrentRostersAsync_WithValidData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();
            var newMatchday = this.Context.Matchdays.Last();

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .SetCurrentRostersAsync(newMatchday.Id)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void SetCurrentRostersAsync_WithInvalidMatchdayId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
               .SetCurrentRostersAsync(Guid.NewGuid())
               .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void SetCurrentRostersAsync_WithEmptyMatchdayId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();
            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
               .SetCurrentRostersAsync(Guid.Empty)
               .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void GetAllUserRosters_WithValidData_ShouldReturnSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .GetAllUserRosters(user.UserName);

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();
            resultCount.ShouldBe(user.Rosters.Count);
            this.TearDown();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ADMIN")]
        [InlineData("aDmIn")]
        [InlineData("       ")]
        [InlineData("  ")]
        [InlineData("SomeName")]
        [InlineData("12315651")]
        [InlineData("sdkjfns-skdfkjsnfk-56544-sdfs")]
        [InlineData("kek")]
        public void GetAllUserRosters_WithInvalidData_ShouldReturnEmpty(string name)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays.First();

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .GetAllUserRosters(name);

            result.ShouldBeEmpty();
            this.TearDown();
        }

        [Fact]
        public void GetCurrentUserRoster_WithValidData_ShouldReturnRosterViewModel()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays
                .First(x => x.MatchdayStatus == MatchdayStatus.Current);

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .GetCurrentUserRoster(user.UserName);

            var model = this.mapper.Map<Roster>(roster);

            result.ShouldNotBeNull();
            model.ShouldBeSameAs(roster);
            this.TearDown();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ADMIN")]
        [InlineData("aDmIn")]
        [InlineData("       ")]
        [InlineData("  ")]
        [InlineData("SomeName")]
        [InlineData("12315651")]
        [InlineData("sdkjfns-skdfkjsnfk-56544-sdfs")]
        [InlineData("kek")]
        public void GetCurrentUserRoster_WithValidData_ShouldReturnNull(string name)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var matchday = this.Context.Matchdays
                .First(x => x.MatchdayStatus == MatchdayStatus.Current);

            var user = this.Context.Users.First();
            var roster = this.CreateRoster(user, matchday);

            this.Context.Rosters.Add(roster);
            this.Context.SaveChanges();

            var result = this.rostersService
                .GetCurrentUserRoster(name);
            
            result.ShouldBeNull();
            this.TearDown();
        }
    }
}

