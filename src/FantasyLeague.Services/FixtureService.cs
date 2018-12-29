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
    public class FixtureService : BaseService, IFixtureService
    {
        private readonly IRepository<Fixture> fixtureRepository;
        private readonly IScoreService scoreService;

        public FixtureService(
            IRepository<Fixture> fixtureRepository,
            IScoreService scoreService,
            IMapper mapper) : base(mapper)
        {
            this.fixtureRepository = fixtureRepository;
            this.scoreService = scoreService;
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

            await fixtureRepository.SaveChangesAsync();

            result.Success = true;

            return result;
        }

        public async Task<IServiceResult> AddPlayerScores(
            Guid fixtureId,
            ICollection<ScoreViewModel> models)
        {
            var result = new ServiceResult { Success = false };

            var fixture = await this.fixtureRepository
                .GetByIdAsync(fixtureId);

            if (fixture == null)
            {
                result.Error = string.Format(
                    ExceptionConstants.NotFoundException,
                    GlobalConstants.FixtureName);

                return result;
            }

            foreach (var scoreModel in models)
            {
                var createResult = await this.scoreService.Create(
                    fixtureId,
                    scoreModel);

                if (!createResult.Success)
                {
                    result.Error = createResult.Error;
                    return result;
                }
            }

            result.Success = true;
            return result;

        }
    }
}
