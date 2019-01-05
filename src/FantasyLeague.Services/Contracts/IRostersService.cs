using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IRostersService
    {
        RosterViewModel GetCurrentUserRoster(string username, Guid matchdayId);

        ICollection<RosterViewModel> GetAllUserRosters(string username);

        Task<IServiceResult> Create(string username, string[] playerIds);
        
        Task<IServiceResult> Edit(Guid rosterId,ICollection<RosterPlayerViewModel> players);

        Task<IServiceResult> SetCurrentRosters(Guid matchdayId);


    }
}
