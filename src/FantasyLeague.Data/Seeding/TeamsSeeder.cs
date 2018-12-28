using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Dtos;
using FantasyLeague.Data.Seeding.Contracts;
using FantasyLeague.Models;
using Newtonsoft.Json;

namespace FantasyLeague.Data.Seeding
{
    public class TeamsSeeder : ISeeder
    {
        private const string ClubsJsonPath = @"..\FantasyLeague.Common\Datasets\clubs.json";

        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            if (!context.Teams.Any())
            {
                string jsonString = File.ReadAllText(ClubsJsonPath);
                SeedTeams(context, jsonString);
            }
        }

        private void SeedTeams(FantasyLeagueDbContext context, string jsonString)
        {
            var deserializedTeams = JsonConvert.DeserializeObject<TeamDto[]>(jsonString);

            var teams = new List<Team>();

            foreach (var item in deserializedTeams)
            {
                var teamImage = new TeamImage
                {
                    PublicId = item.PublicId,
                    Url = item.Url,
                    ImageType = GlobalConstants.TeamName
                };

                var team = new Team
                {
                    Name = item.Name,
                    Initials = item.Initials,
                    TeamImageId = teamImage.Id,
                    TeamImage = teamImage
                };
                teams.Add(team);
            }

            context.Teams.AddRange(teams);

        }
    }
}
