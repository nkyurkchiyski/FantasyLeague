using FantasyLeague.ViewModels.Roster;
using System;
using System.Collections.Generic;

namespace FantasyLeague.Services.Contracts
{
    public interface IRostersService
    {
        RosterViewModel GetCurrentUserRoster(string username, Guid matchdayId);
        
        ICollection<RosterViewModel> GetAllUserRosters(string username);
        
    }
}
