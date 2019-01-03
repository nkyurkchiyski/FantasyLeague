using FantasyLeague.Models;
using FantasyLeague.ViewModels.Roster;
using System;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IUsersService
    {
        IServiceResult ClubNameTaken(string clubName);

        Task<string> GetClubNameAsync(string userId);

        Task<Team> GetFavouriteTeamAsync(string userId);

        Task<IServiceResult> SetFavouriteTeamAsync(string userId, string teamName);

        T GetUser<T>(string username);
        
    }
}
