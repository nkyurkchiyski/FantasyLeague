using FantasyLeague.ViewModels.Score;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IScoreService
    {
        Task<IServiceResult> Create(
            Guid fixtureId,
            ScoreViewModel model);

        //Task<IServiceResult> Edit(
        //    Guid scoreId,
        //    ScoreViewModel model);

        Task<IServiceResult> Delete(Guid scoreId);

        ICollection<T> GetAllPlayerScores<T>(Guid playerId);
        
        ICollection<T> GetAllFixtureScores<T>(Guid fixtureId);

    }
}
