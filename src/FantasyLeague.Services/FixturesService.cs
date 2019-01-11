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
    public class FixturesService : BaseService, IFixturesService
    {
        private readonly IRepository<Fixture> fixturesRepository;
        private readonly IRepository<Score> scoresRepository;

        public FixturesService(
            IRepository<Fixture> fixturesRepository,
            IRepository<Score> scoresRepository,
            IMapper mapper) : base(mapper)
        {
            this.fixturesRepository = fixturesRepository;
            this.scoresRepository = scoresRepository;
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
            switch (fixture.Winner)
            {
                case MatchResult.HomeTeam:
                    return teamId == fixture.HomeTeamId;
                case MatchResult.AwayTeam:
                    return teamId == fixture.AwayTeamId;
                default:
                case MatchResult.Unknown:
                case MatchResult.Draw:
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

        private async Task<IServiceResult> AddPlayerScores(
            Guid fixtureId,
            ICollection<ScoreViewModel> models)
        {
            var result = new ServiceResult { Succeeded = false };

            var fixture = await this.fixturesRepository.GetByIdAsync(fixtureId);

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

            this.scoresRepository.AddRange(scores);

            result.Succeeded = true;
            return result;

        }

        private bool IsEdited(
            Fixture fixture,
            DateTime date,
            FixtureStatus status,
            int homeTeamGoals,
            int awayTeamGoals)
        {
            return fixture.Date != date ||
                   fixture.Status != status ||
                   fixture.HomeTeamGoals != homeTeamGoals ||
                   fixture.AwayTeamGoals != awayTeamGoals;
        }

        public ICollection<T> All<T>()
        {
            var fixtures = this.fixturesRepository.All();

            var models = fixtures.Select(x => this.mapper.Map<T>(x))
                .ToList();

            return models;
        }

        public T GetFixture<T>(Guid fixtureId)
        {
            var fixture = this.fixturesRepository
                .GetByIdAsync(fixtureId)
                .GetAwaiter()
                .GetResult();

            var model = this.mapper.Map<T>(fixture);

            return model;
        }

        public async Task<IServiceResult> EditAsync(
            Guid fixtureId,
            DateTime date,
            FixtureStatus status,
            int homeTeamGoals,
            int awayTeamGoals)
        {
            var result = new ServiceResult { Succeeded = false };

            if (fixtureId == Guid.Empty ||
                date == null ||
                homeTeamGoals < 0 ||
                awayTeamGoals < 0)
            {
                result.Error = string.Format(ExceptionConstants.InvalidInputException);
                return result;
            }

            var fixture = await this.fixturesRepository.GetByIdAsync(fixtureId);

            if (fixture == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.FixtureName);
                return result;
            }

            if (!IsEdited(fixture, date, status, homeTeamGoals, awayTeamGoals))
            {
                result.Succeeded = true;
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

            this.scoresRepository.DeleteRange(fixture.Scores.ToArray());

            await fixturesRepository.SaveChangesAsync();

            result.Succeeded = true;

            return result;
        }

        public async Task<IServiceResult> GenerateScoresAsync(Guid matchdayId)
        {
            var result = new ServiceResult { Succeeded = false };

            var fixtures = this.fixturesRepository.All()
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

            int matchdayWeek = fixtures.First().Matchday.Week;
            foreach (var f in fixtures)
            {
                var scores = new List<ScoreViewModel>();

                try
                {
                    scores.AddRange(TeamScoresGenerator
                        .GenerateTeamPlayerScores(matchdayWeek,
                                                  f.HomeTeamGoals.Value,
                                                  f.AwayTeamGoals.Value,
                                                  f.HomeTeam));

                    scores.AddRange(TeamScoresGenerator
                       .GenerateTeamPlayerScores(matchdayWeek,
                                                 f.AwayTeamGoals.Value,
                                                 f.HomeTeamGoals.Value,
                                                 f.AwayTeam));
                }
                catch (Exception e)
                {
                    result.Error = e.Message;
                    return result;
                }

                var addResult = await this.AddPlayerScores(f.Id, scores);

                if (!addResult.Succeeded)
                {
                    result.Error = addResult.Error;
                    return result;
                }
            }

            await this.scoresRepository.SaveChangesAsync();

            result.Succeeded = true;
            return result;
        }
        
    }
}