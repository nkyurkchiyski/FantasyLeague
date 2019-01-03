using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.User;

namespace FantasyLeague.ViewModels.Index
{
    public class IndexViewModel
    {
        public UserViewModel User { get; set; }

        public int MarchdayWeek { get; set; }

        public TransferWindowStatus TransferWindowStatus { get; set; }
    }
}
