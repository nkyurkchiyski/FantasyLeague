using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IFixtureService
    {
        ICollection<T> All<T>();

        ICollection<T> GetFixturesFromMatchday<T>(Guid matchdayId);

        T Details<T>(Guid fixtureId);

        Task<IServiceResult> Edit(
            Guid fixtureId,
            DateTime date,
            string status,
            int homeTeamGoals,
            int awayTeamGoals);

        Task<IServiceResult> AddPlayerScores(
            Guid fixtureId, 
            ICollection<ScoreViewModel> models);
    }
}
