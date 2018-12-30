using AutoMapper;
using FantasyLeague.Common.Constants;
using FantasyLeague.Data.Repositories.Contracts;
using FantasyLeague.Generators;
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
    public class FixtureService : BaseService, IFixtureService
    {
        private readonly IRepository<Fixture> fixtureRepository;
        private readonly IRepository<Score> scoreRepository;

        public FixtureService(
            IRepository<Fixture> fixtureRepository,
            IRepository<Score> scoreRepository,
            IMapper mapper) : base(mapper)
        {
            this.fixtureRepository = fixtureRepository;
            this.scoreRepository = scoreRepository;
        }

        private MatchResult GetWinner(int homeTeamGoals, int awayTeamGoals)
        {
            if (homeTeamGoals > awayTeamGoals)
            {
                return MatchResult.HomeTeam;
            }
            else if (homeTeamGoals < awayTeamGoals)
            {
                return MatchResult.AwayTeam;
            }
            else
            {
                return MatchResult.Draw;
            }
        }

        private int GetGoalsConceded(Guid teamId, Fixture fixture)
        {
            if (teamId == fixture.HomeTeamId)
            {
                return fixture.AwayTeamGoals.Value;
            }
            else
            {
                return fixture.HomeTeamGoals.Value;
            }
        }

        private bool? GetFixtureOutcome(Guid teamId, Fixture fixture)
        {
            if (teamId == fixture.HomeTeamId)
            {
                return fixture.Winner == MatchResult.HomeTeam;
            }
            else if (teamId == fixture.HomeTeamId)
            {
                return fixture.Winner == MatchResult.AwayTeam;
            }
            else
            {
                return null;
            }
        }

        private Score CreateScore(Fixture fixture, ScoreViewModel model)
        {
            int goalsConceded = this.GetGoalsConceded(model.TeamId, fixture);
            bool? winner = this.GetFixtureOutcome(model.TeamId, fixture);

            var score = new Score
            {
                FixtureId = fixture.Id,
                PlayerId = model.PlayerId,
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

            return score;
        }

        public ICollection<T> All<T>()
        {
            var fixtures = this.fixtureRepository.All();

            var models = fixtures.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public T GetFixture<T>(Guid fixtureId)
        {
            var fixture = this.fixtureRepository
                .GetByIdAsync(fixtureId)
                .GetAwaiter()
                .GetResult();

            var model = this.mapper.Map<T>(fixture);

            return model;
        }

        public async Task<IServiceResult> Edit(
            Guid fixtureId,
            DateTime date,
            FixtureStatus status,
            int homeTeamGoals,
            int awayTeamGoals)
        {
            var result = new ServiceResult { Success = false };

            var fixture = await this.fixtureRepository.GetByIdAsync(fixtureId);

            if (fixture == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.FixtureName);
                return result;
            }

            fixture.Status = status;
            fixture.Date = date;

            if (status == FixtureStatus.Finished)
            {
                fixture.HomeTeamGoals = homeTeamGoals;
                fixture.AwayTeamGoals = awayTeamGoals;

                var winner = GetWinner(homeTeamGoals, awayTeamGoals);

                fixture.Winner = winner;
            }
            else
            {
                fixture.HomeTeamGoals = null;
                fixture.AwayTeamGoals = null;
                fixture.Winner = MatchResult.Unknown;
            }

            this.scoreRepository.DeleteRange(fixture.Scores.ToArray());

            await fixtureRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        private async Task<IServiceResult> AddPlayerScores(
            Guid fixtureId,
            ICollection<ScoreViewModel> models)
        {
            var result = new ServiceResult { Success = false };

            var fixture = await this.fixtureRepository.GetByIdAsync(fixtureId);

            if (fixture == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.FixtureName);

                return result;
            }

            var scores = models.Where(x => x.PlayedMinutes > 0)
                .Select(x => CreateScore(fixture, x))
                .ToArray();

            this.scoreRepository.AddRange(scores);

            result.Success = true;
            return result;

        }

        public async Task<IServiceResult> GenerateScores(Guid matchdayId)
        {
            var result = new ServiceResult { Success = false };

            var fixtures = this.fixtureRepository.All()
                 .Where(x => x.MatchdayId == matchdayId &&
                             x.Status == FixtureStatus.Finished &&
                             !x.Scores.Any());

            if (!fixtures.Any())
            {
                result.Error = string.Format(
                    ExceptionConstants.AlreadyGeneratedException,
                    matchdayId);
                return result;
            }

            foreach (var f in fixtures)
            {
                var scores = new List<ScoreViewModel>();
                try
                {
                    scores.AddRange(TeamScoresGenerator
                        .GenerateTeamPlayerScores(f.HomeTeamGoals.Value,
                                                  f.AwayTeamGoals.Value,
                                                  f.HomeTeam));

                    scores.AddRange(TeamScoresGenerator
                       .GenerateTeamPlayerScores(f.AwayTeamGoals.Value,
                                                 f.HomeTeamGoals.Value,
                                                 f.AwayTeam));
                }
                catch (Exception e)
                {
                    result.Error = e.Message;
                    return result;
                }

                var addResult = await this.AddPlayerScores(f.Id, scores);

                if (!addResult.Success)
                {
                    result.Error = addResult.Error;
                    return result;
                }
            }

            await this.scoreRepository.SaveChangesAsync();

            result.Success = true;
            return result;
        }


    }
}