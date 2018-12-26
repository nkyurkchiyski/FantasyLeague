using FantasyLeague.Data.Seeding.Contracts;
using FantasyLeague.Data.Seeding.Dtos;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FantasyLeague.Data.Seeding
{
    public class MatchdaysSeeder : ISeeder
    {
        private const string MatchdaysJsonPath = @"..\FantasyLeague.Common\Datasets\matchdays.json";

        public void Seed(FantasyLeagueDbContext context, IServiceProvider serviceProvider)
        {
            if (!context.Matchdays.Any())
            {
                string jsonString = File.ReadAllText(MatchdaysJsonPath);
                this.SeedMatchdays(context, jsonString);
            }
        }

        private void SeedMatchdays(FantasyLeagueDbContext context, string jsonString)
        {
            var deserializedMatchdays = JsonConvert.DeserializeObject<MatchdayDto[]>(jsonString);

            var matchdays = new List<Matchday>();

            foreach (var md in deserializedMatchdays)
            {
                var matchday = new Matchday
                {
                    Week = md.Week,
                    StartDate = md.StartDate,
                    EndDate = md.EndDate
                };

                foreach (var f in md.Fixtures)
                {
                    var homeTeam = context.Teams.FirstOrDefault(x => x.Name == f.HomeTeam);
                    var awayTeam = context.Teams.FirstOrDefault(x => x.Name == f.AwayTeam);
                    var status = (FixtureStatus)Enum.Parse(typeof(FixtureStatus), f.Status);

                    var isValid = Enum.TryParse(f.Winner, out MatchResult winner);

                    if (!isValid)
                    {
                        winner = MatchResult.Unknown;
                    }

                    var fixture = new Fixture
                    {
                        HomeTeamId = homeTeam.Id,
                        AwayTeamId = awayTeam.Id,
                        AwayTeamGoals = f.AwayTeamGoals,
                        HomeTeamGoals = f.HomeTeamGoals,
                        Date = f.Date,
                        Status = status,
                        Winner = winner
                    };

                    matchday.Fixtures.Add(fixture);
                }

                matchdays.Add(matchday);
            }

            context.Matchdays.AddRange(matchdays);
        }
    }
}
