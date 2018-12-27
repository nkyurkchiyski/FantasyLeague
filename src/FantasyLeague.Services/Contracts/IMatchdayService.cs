using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IMatchdayService
    {
        ICollection<T> All<T>();

        T Details<T>(Guid matchdayId);

        int Count();

        T GetCurrentMatchday<T>();

    }
}
