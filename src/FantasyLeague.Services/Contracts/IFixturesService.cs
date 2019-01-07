using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IFixturesService
    {
        ICollection<T> All<T>();

        T GetFixture<T>(Guid fixtureId);
        
        Task<IServiceResult> EditAsync(
            Guid fixtureId,
            DateTime date,
            FixtureStatus status,
            int homeTeamGoals,
            int awayTeamGoals);

        Task<IServiceResult> GenerateScoresAsync(Guid matchdayId);
    }
}
