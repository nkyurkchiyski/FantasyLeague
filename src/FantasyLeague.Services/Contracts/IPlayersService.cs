using FantasyLeague.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IPlayersService
    {
        ICollection<T> All<T>();

        ICollection<T> AllFromTeam<T>(Guid teamId);

        Task<T> GetPlayer<T>(Guid playerId);

        Task<IServiceResult> Create(PlayerDetailedViewModel model);

        Task<IServiceResult> Edit(PlayerDetailedViewModel model);

        Task<IServiceResult> Archive(Guid playerId);

        Task<IServiceResult> Restore(Guid playerId);
    }
}
