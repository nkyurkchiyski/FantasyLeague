using FantasyLeague.Models;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IMatchdaysService
    {
        ICollection<T> All<T>();

        T GetMatchday<T>(Guid matchdayId);
        
        Task<Matchday> SetCurrentMatchdayAsync(
            int week, 
            TransferWindowStatus transferWindowStatus);
        
        T GetCurrentMatchday<T>();

    }
}
