using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IFixtureService
    {
        ICollection<T> All<T>();

        T GetFixture<T>(Guid fixtureId);
        
        Task<IServiceResult> Edit(
            Guid fixtureId,
            DateTime date,
            FixtureStatus status,
            int homeTeamGoals,
            int awayTeamGoals);

        Task<IServiceResult> GenerateScores(Guid matchdayId);
    }
}
