using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyLeague.Generators
{
    public static class StatisticsGenerator
    {
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

        public static void GenerateGoalStats(
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
        
        public static List<ScoreViewModel> GenerateSubstitutions(
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

        public static void GenerateRedCards(Random random, ScoreViewModel model)
        {
            model.RedCards = 0;
            if (random.Next(0, ScoreConstants.RedCardChance) == 0)
            {
                model.RedCards = 1;
                model.PlayedMinutes = random.Next(1, ScoreConstants.MaxPlayedMinutesValue);
            }
        }

        public static void GenerateYellowCards(Random random, ScoreViewModel model)
        {
            model.YellowCards = 0;
            if (random.Next(0, ScoreConstants.YellowCardChance) == 0)
            {
                model.YellowCards = 1;
            }
        }

        public static void GenerateTackles(
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

        public static void GenerateShots(
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
    }
}
