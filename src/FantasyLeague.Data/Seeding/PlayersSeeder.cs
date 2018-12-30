﻿using FantasyLeague.Common.Constants;
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
        private const string TemplatePlayerImagePublicId = "FantasyLeague/Players/default.png";
        private const string TemplatePlayerImageUrl = @"https://res.cloudinary.com/nkyurkchiyski/image/upload/v1545754511/FantasyLeague/Players/default.png";

        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            if (!context.Players.Any())
            {
                for (int i = 1; i <= GlobalConstants.TeamNumber; i++)
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

            foreach (var pl in deserializedSquad.Players)
            {
                var isValidPosition = Enum.TryParse(pl.Position, out PlayerPosition position);

                if (isValidPosition)
                {
                    var playerImage = new Image
                    {
                        ImageType = GlobalConstants.PlayerName,
                        PublicId = TemplatePlayerImagePublicId,
                        Url = TemplatePlayerImageUrl
                    };

                    var player = new Player
                    {
                        TeamId = team.Id,
                        Name = pl.Name,
                        Nationality = pl.Nationality,
                        Position = position,
                        PlayerImageId = playerImage.Id,
                        PlayerImage = playerImage
                    };

                    players.Add(player);
                }
            }

            context.AddRange(players);
        }
    }
}
