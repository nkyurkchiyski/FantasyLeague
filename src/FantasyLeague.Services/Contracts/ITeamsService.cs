using System;
using System.Collections.Generic;

namespace FantasyLeague.Services.Contracts
{
    public interface ITeamsService
    {
        ICollection<T> All<T>();

        T GetTeamById<T>(Guid teamId);

        T GetTeamByName<T>(string teamName);
    }
}
