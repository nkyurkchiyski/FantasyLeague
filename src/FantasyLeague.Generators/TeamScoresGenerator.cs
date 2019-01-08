using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyLeague.Generators
{
    public static class TeamScoresGenerator
    {
        private static void GenerateScoreViewModels(
            Random random,
            int goals,
            int goalsConceded,
            List<ScoreViewModel> models)
        {
            StatisticsGenerator.GenerateGoalStats(random, goals, models);
            foreach (var model in models)
            {
                StatisticsGenerator.GenerateShots(random, goals, model);
                StatisticsGenerator.GenerateTackles(random, goalsConceded, model);
                StatisticsGenerator.GenerateYellowCards(random, model);
                StatisticsGenerator.GenerateRedCards(random, model);
            }
        }

        private static List<ScoreViewModel> CreateScoreViewModels(
           Random random,
           int matchdayWeek,
           int[] teamLayout,
           int subs,
           Team team)
        {
            var playerModels = new List<ScoreViewModel>();
            var selectedPlayers = new List<Player>();

            for (int i = teamLayout.Length; i > 0; i--)
            {
                var models = new List<ScoreViewModel>();
                var playerForPosition = SquadGenerator.GeneratePlayersForPosition(
                                                random,
                                                matchdayWeek,
                                                teamLayout[teamLayout.Length - i],
                                                team,
                                                (PlayerPosition)(i + ScoreConstants.PlayerPosParam));

                foreach (var pl in playerForPosition)
                {
                    models.Add(new ScoreViewModel
                    {
                        PlayerId = pl.Id,
                        PlayedMinutes = ScoreConstants.MaxPlayedMinutesValue,
                        Position = pl.Position,
                        TeamId = pl.TeamId
                    });
                }
                playerModels.AddRange(models);
                selectedPlayers.AddRange(playerForPosition);
            }

            var subModels = new List<ScoreViewModel>();
            var subPlayers = SquadGenerator.GetSubPlayers(random, subs, selectedPlayers, team);

            foreach (var pl in subPlayers)
            {
                subModels.Add(new ScoreViewModel
                {
                    PlayerId = pl.Id,
                    PlayedMinutes = ScoreConstants.MaxPlayedMinutesValue,
                    Position = pl.Position,
                    TeamId = pl.TeamId
                });
            }
            playerModels.AddRange(subModels);

            return playerModels;
        }

        public static List<ScoreViewModel> GenerateTeamPlayerScores(
            int matchdayWeek,
            int goals,
            int goalsConceded,
            Team team)
        {
            var random = new Random();

            int subs = random.Next(ScoreConstants.MaxSubs + 1);
            var squad = SquadGenerator.GenerateRandomSquad(random, matchdayWeek, team);

            var models = CreateScoreViewModels(random, matchdayWeek, squad, subs, team);

            GenerateScoreViewModels(random, goals, goalsConceded, models);
            models = StatisticsGenerator.GenerateSubstitutions(random, subs, models);

            return models.Distinct().ToList();
        }
        
    }
}