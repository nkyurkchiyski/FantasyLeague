using FantasyLeague.ViewModels.Roster;
using System.Threading.Tasks;

namespace FantasyLeague.Services.Contracts
{
    public interface ITransfersService
    {
        Task<IServiceResult> TransferPlayersAsync(
            string username,
            string[] playersIn,
            string[] playersOut);

        TransfersRosterViewModel GetTransfersRosterViewModel(string username);
    }
}
