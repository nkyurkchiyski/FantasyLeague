using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyLeague.Generators
{
    public static class TeamScoresGenerator
    {
        public static List<ScoreViewModel> GenerateTeamPlayerScores(int goals, int goalsConceded, Team team)
        {
            var random = new Random();

            int subs = random.Next(ScoreConstants.MaxSubs + 1);
            var teamLayout = GetRandomTeamLayout(random);
            teamLayout = AddSubs(random, subs, teamLayout);

            var models = CreateScoreViewModels(random, teamLayout, team);

            GenerateScoreViewModels(random, goals, goalsConceded, models);
            models = GenerateSubs(random, subs, models);

            return models;
        }

        private static List<ScoreViewModel> GenerateSubs(Random random, int subs, List<ScoreViewModel> models)
        {
            var notSubbedPlayers = models.Where(x =>
                                                    x.Position != PlayerPosition.Goalkeeper &&
                                                    x.PlayedMinutes == ScoreConstants.MaxPlayedMinutesValue)
                                             .ToList();
            var subbedPlayers = models.Where(x => x.Position == PlayerPosition.Goalkeeper).ToList();

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
            GenerateGoalScorers(random, goals, models);
            GenerateGoalAssisters(random, goals, models);
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

        private static void GenerateTackles(Random random, int goalsConceded, ScoreViewModel model)
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

        private static void GenerateShots(Random random, int goals, ScoreViewModel model)
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
                case PlayerPosition.Goalkeeper:
                    model.Shots = Math.Abs(--shots);
                    break;
            }
        }

        private static void GenerateGoalAssisters(Random random, int assists, List<ScoreViewModel> models)
        {
            int loopCap = 0;
            while (assists > 0)
            {
                if (loopCap == ScoreConstants.LoopCap)
                {
                    models[random.Next(models.Count)].Assists++;
                    assists--;
                    loopCap = 0;
                    continue;
                }

                for (int i = 0; i < models.Count; i++)
                {
                    if (random.Next(0, (int)models[i].Position) == 0)
                    {
                        models[i].Assists++;
                        assists--;

                        if (assists == 0)
                        {
                            return;
                        }
                    }
                }

                loopCap++;

            }
        }

        private static void GenerateGoalScorers(
            Random random,
            int goals,
            List<ScoreViewModel> models)
        {
            int loopCap = 0;

            while (goals > 0)
            {
                if (loopCap == ScoreConstants.LoopCap)
                {
                    models[random.Next(models.Count)].Goals++;
                    goals--;
                    loopCap = 0;
                    continue;
                }

                for (int i = 0; i < models.Count; i++)
                {
                    if (random.Next(0, (int)models[i].Position) == 0)
                    {
                        models[i].Goals++;
                        goals--;

                        if (goals == 0)
                        {
                            return;
                        }
                    }
                }

                loopCap++;

            }
        }

        private static List<ScoreViewModel> CreateScoreViewModels(
            Random random,
            int[] teamLayout,
            Team team)
        {
            var teamModels = new List<ScoreViewModel>();

            for (int i = teamLayout.Length; i > 0; i--)
            {
                var models = new List<ScoreViewModel>();
                var playerForPosition = GetRandomPlayersForPosition(
                                                random,
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
                teamModels.AddRange(models);
            }

            return teamModels;
        }

        private static int[] AddSubs(
            Random random,
            int subs,
            int[] layout)
        {
            for (int i = 0; i < subs; i++)
            {
                layout[random.Next(1, layout.Length)]++;
            }
            return layout;
        }

        private static IEnumerable<Player> GetRandomPlayersForPosition(
            Random random,
            int neededPlayers,
            Team team,
            PlayerPosition playerPosition)
        {
            var playersForPosition = GetPlayersForPosition(team, playerPosition).ToList();
            int playersCount = GetPlayersForPosition(team, playerPosition).Count();

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
                        player = playersForPosition.First(x => selecetedPlayers.Exists(y => x.Id != y.Id));
                        break;
                    }
                }

                selecetedPlayers.Add(player);
            }

            return selecetedPlayers;
        }

        private static IEnumerable<Player> GetPlayersForPosition(
            Team team,
            PlayerPosition playerPosition)
        {
            return team.Players.Where(x => x.Position == playerPosition && x.Active);
        }

        private static int[] GetRandomTeamLayout(Random random)
        {
            string[] allFormations = Enum.GetNames(typeof(Formation));
            string formation = allFormations[random.Next(0, ScoreConstants.MaxFormations)];

            var layout = new List<int> { 1 };

            layout.AddRange(formation.Replace(ScoreConstants.Formation, "").ToCharArray()
                 .Select(n => int.Parse(n.ToString())));

            return layout.ToArray();
        }
    }
}

