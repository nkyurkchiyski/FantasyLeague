using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Data.Seeding;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Player;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class PlayersServiceTests : BaseServiceTests
    {
        private readonly IPlayersService playersService;

        public PlayersServiceTests()
        {
            var playesRepository = this.Provider.GetService<IRepository<Player>>();
            var teamsRepository = this.Provider.GetService<IRepository<Team>>();
            var automapper = this.Provider.GetService<IMapper>();
            var imagesService = new Mock<IImagesService>();

            this.playersService = new PlayersService(imagesService.Object,
                playesRepository,
                teamsRepository,
                automapper);
        }

        [Fact]
        public void All_WithExistingData_ShouldReturnTheSameSize()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var allPlayers = this.Context.Players.ToList();

            var result = this.playersService
                .All<Player>().ToList();

            var resultCount = result.Count;

            result.ShouldNotBeEmpty();

            resultCount.ShouldBe(allPlayers.Count);
            this.TearDown();
        }

        [Fact]
        public void All_WithoutExistingData_ShouldReturnEmpty()
        {
            var result = this.playersService
                .All<Player>().ToList();

            result.ShouldBeEmpty();

            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithVaildData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var model = new PlayerDetailedViewModel
            {
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = team.Id
            };

            var result = this.playersService.CreateAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Theory]
        [InlineData("", -4, "")]
        [InlineData("", 0, "")]
        [InlineData("SomeName", 4, "")]
        [InlineData("", 4, "SomeNation")]
        [InlineData(" ", 4, "  ")]
        [InlineData("       ", 4, "     ")]
        [InlineData(null, 4, "KEk")]
        [InlineData("Lel", 4, null)]
        [InlineData(null, 4, null)]
        [InlineData("SomeName", int.MinValue, "SomeNation")]
        [InlineData("SomeName", int.MaxValue, "SomeNation")]
        [InlineData("SomeName", 0, "SomeNation")]
        [InlineData("SomeName", -1, "SomeNation")]
        [InlineData("SomeName", 26, "SomeNation")]
        public void CreateAsync_WithInvaildData_ShouldReturnFalse(
            string name, double price, string nationality)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();

            var model = new PlayerDetailedViewModel
            {
                Name = name,
                Price = price,
                Image = null,
                Nationality = nationality,
                TeamId = team.Id
            };

            var result = this.playersService.CreateAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithInvalidTeamId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var model = new PlayerDetailedViewModel
            {
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = Guid.NewGuid()
            };

            var result = this.playersService.CreateAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void CreateAsync_WithEmptyTeamId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var model = new PlayerDetailedViewModel
            {
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = Guid.Empty
            };

            var result = this.playersService.CreateAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Theory]
        [InlineData("", -4, "")]
        [InlineData("", 0, "")]
        [InlineData("SomeName", 4, "")]
        [InlineData("", 4, "SomeNation")]
        [InlineData(" ", 4, "  ")]
        [InlineData("       ", 4, "     ")]
        [InlineData(null, 4, "KEk")]
        [InlineData("Lel", 4, null)]
        [InlineData(null, 4, null)]
        [InlineData("SomeName", int.MinValue, "SomeNation")]
        [InlineData("SomeName", int.MaxValue, "SomeNation")]
        [InlineData("SomeName", 0, "SomeNation")]
        [InlineData("SomeName", -1, "SomeNation")]
        [InlineData("SomeName", 26, "SomeNation")]
        public void EditAsync_WithInvaildData_ShouldReturnFalse(
            string name, double price, string nationality)
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();
            var player = this.Context.Players.First();

            var model = new PlayerDetailedViewModel
            {
                Id = player.Id,
                Name = name,
                Price = price,
                Image = null,
                Nationality = nationality,
                TeamId = team.Id
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithInvalidTeamId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var player = this.Context.Players.First();

            var model = new PlayerDetailedViewModel
            {
                Id = player.Id,
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = Guid.NewGuid()
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithEmptyTeamId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var player = this.Context.Players.First();

            var model = new PlayerDetailedViewModel
            {
                Id = player.Id,
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = Guid.Empty
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithInvalidPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var team = this.Context.Teams.First();

            var model = new PlayerDetailedViewModel
            {
                Id = Guid.NewGuid(),
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = team.Id
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithEmptyPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            var team = this.Context.Teams.First();

            var model = new PlayerDetailedViewModel
            {
                Id = Guid.Empty,
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = team.Id
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeFalse();
            this.TearDown();
        }

        [Fact]
        public void EditAsync_WithVaildData_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var team = this.Context.Teams.First();
            var player = this.Context.Players.First();

            var model = new PlayerDetailedViewModel
            {
                Id = player.Id,
                Name = "SomeName",
                Price = 20,
                Image = null,
                Nationality = "SomeNation",
                TeamId = team.Id
            };

            var result = this.playersService.EditAsync(model)
                .GetAwaiter().GetResult();

            result.Succeeded.ShouldBeTrue();
            this.TearDown();
        }

        [Fact]
        public void GetPlayerAsync_WithVaildPlayerId_ShouldReturnPlayer()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var player = this.Context.Players.First();

            var result = this.playersService
                .GetPlayerAsync<Player>(player.Id)
                .GetAwaiter()
                .GetResult();

            result.ShouldBeSameAs(player);

            this.TearDown();
        }

        [Fact]
        public void GetPlayerAsync_WithInvaildPlayerId_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);
            
            var result = this.playersService
                .GetPlayerAsync<Player>(Guid.NewGuid())
                .GetAwaiter()
                .GetResult();

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void GetPlayerAsync_WithEmptyPlayerId_ShouldReturnNull()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.playersService
                .GetPlayerAsync<Player>(Guid.Empty)
                .GetAwaiter()
                .GetResult();

            result.ShouldBeNull();

            this.TearDown();
        }

        [Fact]
        public void ArchiveAsync_WithVaildPlayerId_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var player = this.Context.Players.First();

            var result = this.playersService
                .ArchiveAsync(player.Id)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeTrue();

            this.TearDown();
        }

        [Fact]
        public void ArchiveAsync_WithInvaildPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.playersService
                .ArchiveAsync(Guid.NewGuid())
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void ArchiveAsync_WithEmptyPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.playersService
                .ArchiveAsync(Guid.Empty)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void RestoreAsync_WithVaildPlayerId_ShouldReturnTrue()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var player = this.Context.Players.First();

            var result = this.playersService
                .RestoreAsync(player.Id)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeTrue();

            this.TearDown();
        }

        [Fact]
        public void RestoreAsync_WithInvaildPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.playersService
                .RestoreAsync(Guid.NewGuid())
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

        [Fact]
        public void RestoreAsync_WithEmptyPlayerId_ShouldReturnFalse()
        {
            FantasyLeagueDbContextSeeder.Seed(Context, Provider);

            var result = this.playersService
                .RestoreAsync(Guid.Empty)
                .GetAwaiter()
                .GetResult();

            result.Succeeded.ShouldBeFalse();

            this.TearDown();
        }

    }
}
