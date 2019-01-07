using FantasyLeague.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IPlayersService
    {
        ICollection<T> All<T>();
        
        Task<T> GetPlayerAsync<T>(Guid playerId);

        Task<IServiceResult> CreateAsync(PlayerDetailedViewModel model);

        Task<IServiceResult> EditAsync(PlayerDetailedViewModel model);

        Task<IServiceResult> ArchiveAsync(Guid playerId);

        Task<IServiceResult> RestoreAsync(Guid playerId);
    }
}
