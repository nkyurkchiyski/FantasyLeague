using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IRostersService
    {
        RosterViewModel GetCurrentUserRoster(string username);

        ICollection<RosterViewModel> GetAllUserRosters(string username);

        Task<IServiceResult> CreateAsync(string username, string[] playerIds);

        Task<IServiceResult> EditAsync(ICollection<RosterPlayerViewModel> players);

        Task<IServiceResult> SetCurrentRostersAsync(Guid matchdayId);

        Task<IServiceResult> SetNewFormationAsync(Formation formation, Guid rosterId);
    }
}
