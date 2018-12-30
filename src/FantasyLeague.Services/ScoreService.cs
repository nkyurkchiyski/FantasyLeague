using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using FantasyLeague.Services.Contracts;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Services
{
    public class ScoreService : BaseService, IScoreService
    {
        private readonly IRepository<Score> scoreRepository;
        private readonly IRepository<Fixture> fixtureRepository;
        private readonly IRepository<Player> playerRepository;

        public ScoreService(
            IRepository<Score> scoreRepository,
            IRepository<Fixture> fixtureRepository,
            IRepository<Player> playerRepository,
            IMapper mapper) : base(mapper)
        {
            this.scoreRepository = scoreRepository;
            this.fixtureRepository = fixtureRepository;
            this.playerRepository = playerRepository;
        }

        private int GetGoalsConceded(Player player, Fixture fixture)
        {
            if (player.Team == fixture.HomeTeam)
            {
                return fixture.AwayTeamGoals.Value;
            }
            else
            {
                return fixture.HomeTeamGoals.Value;
            }
        }

        private bool? GetFixtureOutcome(Player player, Fixture fixture)
        {
            if (player.Team == fixture.HomeTeam)
            {
                return fixture.Winner == MatchResult.HomeTeam;
            }
            else if (player.Team == fixture.HomeTeam)
            {
                return fixture.Winner == MatchResult.AwayTeam;
            }
            else
            {
                return null;
            }
        }

        public async Task<IServiceResult> Create(
            Guid fixtureId,
            ScoreViewModel model)
        {
            var result = new ServiceResult { Success = false };

            var fixture = await this.fixtureRepository.GetByIdAsync(fixtureId);
            var player = await this.playerRepository.GetByIdAsync(model.PlayerId);

            if (fixture == null || fixture.Status != FixtureStatus.Finished)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.FixtureName);
                return result;
            }

            if (player == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.PlayerName);
                return result;
            }


            int goalsConceded = this.GetGoalsConceded(player, fixture);
            bool? winner = this.GetFixtureOutcome(player, fixture);

            var score = new Score
            {
                FixtureId = fixture.Id,
                PlayerId = player.Id,
                Shots = model.Shots,
                Assists = model.Assists,
                Goals = model.Goals,
                Tackles = model.Tackles,
                YellowCards = model.YellowCards,
                RedCards = model.RedCards,
                PlayedMinutes = model.PlayedMinutes,
                GoalsConceded = goalsConceded,
                CleanSheet = goalsConceded == 0,
                IsWiner = winner
            };

            this.scoreRepository.Add(score);
            await this.scoreRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        public async Task<IServiceResult> Delete(Guid scoreId)
        {
            var result = new ServiceResult { Success = false };

            var score = await this.scoreRepository.GetByIdAsync(scoreId);

            if (score == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.ScoreName);
                return result;
            }

            this.scoreRepository.Delete(score);
            await this.scoreRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        public async Task<IServiceResult> Edit(
            Guid scoreId,
            ScoreViewModel model)
        {
            var result = new ServiceResult { Success = false };

            var score = await this.scoreRepository.GetByIdAsync(scoreId);

            if (score == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.ScoreName);
                return result;
            }

            int goalsConceded = this.GetGoalsConceded(score.Player, score.Fixture);
            bool? winner = this.GetFixtureOutcome(score.Player, score.Fixture);

            score.Shots = model.Shots;
            score.Assists = model.Assists;
            score.Goals = model.Goals;
            score.Tackles = model.Tackles;
            score.PlayedMinutes = model.PlayedMinutes;
            score.GoalsConceded = goalsConceded;
            score.CleanSheet = goalsConceded == 0;
            score.IsWiner = winner;

            try
            {
                await this.scoreRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Error = e.Message;
                return result;
            }

            result.Success = true;

            return result;
        }

        public ICollection<T> GetAllFixtureScores<T>(Guid fixtureId)
        {
            var fixtureScores = this.scoreRepository.All()
                .Where(x => x.FixtureId == fixtureId);

            var models = fixtureScores.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public ICollection<T> GetAllPlayerScores<T>(Guid playerId)
        {
            var playerScores = this.scoreRepository.All()
               .Where(x => x.PlayerId == playerId);

            var models = playerScores.Select(x => this.mapper.Map<T>(x))
               .ToList();

            return models;
        }
    }
}
