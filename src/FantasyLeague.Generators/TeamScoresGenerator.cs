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
        public static List<ScoreViewModel> GenerateTeamPlayerScores(
            int matchdayWeek,
            int goals,
            int goalsConceded,
            Team team)
        {
            var random = new Random();

            int subs = random.Next(ScoreConstants.MaxSubs + 1);
            var teamLayout = GetRandomTeamLayout(random, matchdayWeek, team);

            var models = CreateScoreViewModels(random, matchdayWeek, teamLayout, subs, team);

            GenerateScoreViewModels(random, goals, goalsConceded, models);
            models = GenerateSubs(random, subs, models);

            return models.Distinct().ToList();
        }

        private static List<ScoreViewModel> GenerateSubs(
            Random random,
            int subs,
            List<ScoreViewModel> models)
        {
            var notSubbedPlayers = models.Where(x =>
                                                    x.Position != PlayerPosition.Goalkeeper &&
                                                    x.PlayedMinutes == ScoreConstants.MaxPlayedMinutesValue)
                                             .ToList();

            var subbedPlayers = models.Where(x => x.Position == PlayerPosition.Goalkeeper ||
                                                  x.RedCards == 1)
                                             .ToList();

            while (subs > 0)
            {
                int playedMinutes = random.Next(0, ScoreConstants.MaxPlayedMinutesValue);
                for (int i = 0; i < 2; i++)
                {
                    int playerToken = random.Next(0, notSubbedPlayers.Count);

                    if (i == 0)
                    {
                        notSubbedPlayers[playerToken].PlayedMinutes = playedMinutes;
                    }
                    else
                    {
                        notSubbedPlayers[playerToken].PlayedMinutes = ScoreConstants.MaxPlayedMinutesValue - playedMinutes;
                    }

                    subbedPlayers.Add(notSubbedPlayers[playerToken]);
                    notSubbedPlayers.RemoveAt(playerToken);
                }

                subs--;
            }

            return notSubbedPlayers.Concat(subbedPlayers).ToList();
        }

        private static void GenerateScoreViewModels(
            Random random,
            int goals,
            int goalsConceded,
            List<ScoreViewModel> models)
        {
            GenerateGoalStats(random, goals, models);
            foreach (var model in models)
            {
                GenerateShots(random, goals, model);
                GenerateTackles(random, goalsConceded, model);
                GenerateYellowCards(random, model);
                GenerateRedCards(random, model);
            }
        }

        private static void GenerateRedCards(Random random, ScoreViewModel model)
        {
            model.RedCards = 0;
            if (random.Next(0, ScoreConstants.RedCardChance) == 0)
            {
                model.RedCards = 1;
                model.PlayedMinutes = random.Next(1, ScoreConstants.MaxPlayedMinutesValue);
            }
        }

        private static void GenerateYellowCards(Random random, ScoreViewModel model)
        {
            model.YellowCards = 0;
            if (random.Next(0, ScoreConstants.YellowCardChance) == 0)
            {
                model.YellowCards = 1;
            }
        }

        private static void GenerateTackles(
            Random random,
            int goalsConceded,
            ScoreViewModel model)
        {
            int tackles = random.Next(0, ScoreConstants.PlayerPosParam);

            if (goalsConceded > ScoreConstants.ScoreBonus)
            {
                tackles++;
            }

            model.Tackles = tackles;

            switch (model.Position)
            {
                case PlayerPosition.Defender:
                    model.Tackles += ScoreConstants.ScoreBonus;
                    break;
                case PlayerPosition.Midfielder:
                    model.Tackles++;
                    break;
                case PlayerPosition.Attacker:
                case PlayerPosition.Goalkeeper:
                    model.Tackles = Math.Abs(--tackles);
                    break;
            }
        }

        private static void GenerateShots(
            Random random,
            int goals,
            ScoreViewModel model)
        {
            int shots = random.Next(0, ScoreConstants.PlayerPosParam);

            if (goals > ScoreConstants.ScoreBonus)
            {
                shots++;
            }

            model.Shots = shots;

            switch (model.Position)
            {
                case PlayerPosition.Attacker:
                    model.Shots += ScoreConstants.ScoreBonus;
                    break;
                case PlayerPosition.Midfielder:
                    model.Shots++;
                    break;
                case PlayerPosition.Defender:
                    model.Shots = Math.Abs(--shots);
                    break;
                case PlayerPosition.Goalkeeper:
                    model.Shots = 0;
                    break;
            }
        }

        private static ScoreViewModel GenerateGoalAssister(
            Random random,
            List<ScoreViewModel> models)
        {
            int loopCap = 0;

            while (true)
            {
                if (loopCap == ScoreConstants.LoopCap)
                {
                    return models[random.Next(models.Count)];
                }

                for (int i = models.Count - 1; i >= 0; i--)
                {
                    if (random.Next(0, (int)models[i].Position) == 0)
                    {
                        return models[i];
                    }
                }
                loopCap++;
            }
        }

        private static ScoreViewModel GenerateGoalScorer(
            Random random,
            List<ScoreViewModel> models)
        {
            int loopCap = 0;

            while (true)
            {
                if (loopCap == ScoreConstants.LoopCap)
                {
                    return models[random.Next(models.Count)];
                }

                for (int i = models.Count - 1; i >= 0; i--)
                {
                    if (random.Next(0, (int)models[i].Position) == 0)
                    {
                        return models[i];
                    }
                }
                loopCap++;
            }
        }

        private static void GenerateGoalStats(
            Random random,
            int goals,
            List<ScoreViewModel> models)
        {

            for (int i = 0; i < goals; i++)
            {
                var goalScorer = GenerateGoalScorer(random, models);
                var goalAssister = GenerateGoalAssister(random, models.Where(x => x.PlayerId != goalScorer.PlayerId).ToList());

                models.Find(x => x.PlayerId == goalScorer.PlayerId).Goals++;
                models.Find(x => x.PlayerId == goalAssister.PlayerId).Assists++;

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
                var playerForPosition = GetRandomPlayersForPosition(
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
            var subPlayers = GetSubPlayers(random, subs, selectedPlayers, team);

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

        private static IEnumerable<Player> GetSubPlayers(
            Random random,
            int subs,
            List<Player> selectedPlayers,
            Team team)
        {
            var remainingPlayers = team.Players
                .Except(selectedPlayers)
                .Where(x => x.Position != PlayerPosition.Goalkeeper)
                .ToList();

            var playersCount = remainingPlayers.Count;

            var selecetedPlayers = new List<Player>();

            for (int i = 0; i < (subs > playersCount ? playersCount : subs); i++)
            {
                var player = remainingPlayers[random.Next(playersCount)];

                int loopCap = 0;
                while (selecetedPlayers.Exists(x => x.Id == player.Id))
                {
                    loopCap++;
                    player = remainingPlayers[random.Next(playersCount)];

                    if (loopCap == ScoreConstants.LoopCap)
                    {
                        player = remainingPlayers.Except(selecetedPlayers).First();
                        break;
                    }
                }

                selecetedPlayers.Add(player);
            }

            return selecetedPlayers;
        }

        private static IEnumerable<Player> GetRandomPlayersForPosition(
            Random random,
            int matchdayWeek,
            int neededPlayers,
            Team team,
            PlayerPosition playerPosition)
        {
            var playersForPosition = GetPlayersForPosition(matchdayWeek, team, playerPosition).ToList();
            int playersCount = playersForPosition.Count;

            var selecetedPlayers = new List<Player>();

            for (int i = 0; i < neededPlayers; i++)
            {
                var player = playersForPosition[random.Next(playersCount)];

                int loopCap = 0;
                while (selecetedPlayers.Exists(x => x.Id == player.Id))
                {
                    loopCap++;
                    player = playersForPosition[random.Next(playersCount)];

                    if (loopCap == ScoreConstants.LoopCap)
                    {
                        var notSelectedPlayers = playersForPosition.Except(selecetedPlayers);
                        player = notSelectedPlayers.First();
                        break;
                    }
                }

                selecetedPlayers.Add(player);
            }

            return selecetedPlayers;
        }

        private static IEnumerable<Player> GetPlayersForPosition(
            int matchdayWeek,
            Team team,
            PlayerPosition playerPosition)
        {
            var allPlayers = team.Players.Where(x => x.Position == playerPosition &&
                                                     x.Active);

            if (matchdayWeek == 1)
            {
                return allPlayers;
            }

            var eligiblePlayers = new List<Player>();

            foreach (var player in allPlayers)
            {
                var scorePreviousMatchday = player.Scores
                    .FirstOrDefault(x => x.Fixture.Matchday.Week == matchdayWeek - 1);

                if (scorePreviousMatchday == null)
                {
                    eligiblePlayers.Add(player);
                }
                else if (scorePreviousMatchday != null &&
                         scorePreviousMatchday.RedCards == 0)
                {
                    eligiblePlayers.Add(player);
                }

            }
            return eligiblePlayers;
        }

        private static int[] GetRandomTeamLayout(
            Random random,
            int matchdayWeek,
            Team team)
        {
            int defendersCount = GetPlayersForPosition(matchdayWeek, team, PlayerPosition.Defender).Count();
            int attackersCount = GetPlayersForPosition(matchdayWeek, team, PlayerPosition.Attacker).Count();
            int halfsCount = GetPlayersForPosition(matchdayWeek, team, PlayerPosition.Midfielder).Count();

            string[] allFormations = Enum.GetNames(typeof(Formation));
            string formation = allFormations[random.Next(0, ScoreConstants.MaxFormations)];

            var layout = new List<int> { 1 };

            layout.AddRange(formation.Replace(ScoreConstants.Formation, "").ToCharArray()
                 .Select(n => int.Parse(n.ToString())));

            if (layout[1] > defendersCount)
            {
                layout = new List<int> { 1 };

                int remainingPlayersCount = ScoreConstants.OutfieldPlayers - defendersCount;
                int mids = remainingPlayersCount / 2;

                layout.AddRange(new[] { defendersCount, mids, remainingPlayersCount - mids });

            }
            else if (layout[2] > halfsCount)
            {
                layout = new List<int> { 1 };

                int remainingPlayersCount = ScoreConstants.OutfieldPlayers - halfsCount;
                int attackers = remainingPlayersCount / 2;

                layout.AddRange(new[] { remainingPlayersCount - attackers, halfsCount, attackers });
            }
            else if (layout[3] > attackersCount)
            {
                layout = new List<int> { 1 };

                int remainingPlayersCount = ScoreConstants.OutfieldPlayers - attackersCount;
                int defenders = remainingPlayersCount / 2;

                layout.AddRange(new[] { defenders, remainingPlayersCount - defenders, attackersCount });

            }

            return layout.ToArray();
        }
    }
}

