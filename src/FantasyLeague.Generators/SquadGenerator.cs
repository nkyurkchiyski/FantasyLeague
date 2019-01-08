using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyLeague.Generators
{
    public static class SquadGenerator
    {
        private static List<Player> GetAvailablePlayersForPosition(
            int matchdayWeek,
            Team team,
            PlayerPosition playerPosition)
        {
            var allPlayers = team.Players.Where(x => x.Position == playerPosition &&
                                                     x.Active).ToList();

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

        public static List<Player> GeneratePlayersForPosition(
           Random random,
           int matchdayWeek,
           int neededPlayers,
           Team team,
           PlayerPosition playerPosition)
        {
            var playersForPosition = GetAvailablePlayersForPosition(matchdayWeek, team, playerPosition);
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

        public static List<Player> GetSubPlayers(
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

        public static int[] GenerateRandomSquad(
            Random random,
            int matchdayWeek,
            Team team)
        {
            int defendersCount = GetAvailablePlayersForPosition(matchdayWeek, team, PlayerPosition.Defender).Count();
            int attackersCount = GetAvailablePlayersForPosition(matchdayWeek, team, PlayerPosition.Attacker).Count();
            int halfsCount = GetAvailablePlayersForPosition(matchdayWeek, team, PlayerPosition.Midfielder).Count();

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
