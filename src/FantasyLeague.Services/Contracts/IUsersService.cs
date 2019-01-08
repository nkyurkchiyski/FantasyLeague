using FantasyLeague.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface IUsersService
    {
        IServiceResult ClubNameVacant(string clubName);

        Task<string> GetClubNameAsync(string userId);

        Task<Team> GetFavouriteTeamAsync(string userId);

        Task<IServiceResult> SetFavouriteTeamAsync(string userId, string teamName);

        T GetUserByUsername<T>(string username);

        ICollection<T> GetLeaderboard<T>();

    }
}
