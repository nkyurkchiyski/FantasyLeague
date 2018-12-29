using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IMatchdayService
    {
        ICollection<T> All<T>();

        T GetMatchday<T>(Guid matchdayId);

        int Count();

        Task<IServiceResult> SetCurrentMatchday(int week);

        Task<IServiceResult> SetTransferWindowStatus(
            string transferWindowStatus);

        T GetCurrentMatchday<T>();

    }
}
