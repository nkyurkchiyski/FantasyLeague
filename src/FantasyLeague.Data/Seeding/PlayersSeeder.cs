using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Seeding.Contracts;
using FantasyLeague.Data.Seeding.Dtos;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FantasyLeague.Data.Seeding
{
    public class PlayersSeeder : ISeeder
    {
        private const string TeamJsonPath = @"..\FantasyLeague.Common\Datasets\squads\{0}.json";

        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            if (!context.Players.Any())
            {
                for (int i = 1; i <= GlobalConstants.TeamsCount; i++)
                {
                    string jsonString = File.ReadAllText(string.Format(TeamJsonPath, i));
                    this.SeedTeam(context, jsonString);
                }
            }
        }

        private void SeedTeam(FantasyLeagueDbContext context, string jsonString)
        {
            var deserializedSquad = JsonConvert.DeserializeObject<SquadDto>(jsonString);

            var team = context.Teams.FirstOrDefault(x => x.Name == deserializedSquad.Name);

            var players = new List<Player>();

            var random = new Random();

            foreach (var pl in deserializedSquad.Players)
            {
                var isValidPosition = Enum.TryParse(pl.Position, out PlayerPosition position);

                if (isValidPosition)
                {
                    var player = new Player
                    {
                        TeamId = team.Id,
                        Name = pl.Name,
                        Nationality = pl.Nationality,
                        Position = position,
                        Price = random.Next(GlobalConstants.MinPlayerPrice, GlobalConstants.MaxPlayerPrice)
                    };

                    players.Add(player);
                }
            }

            context.AddRange(players);
        }
    }
}
