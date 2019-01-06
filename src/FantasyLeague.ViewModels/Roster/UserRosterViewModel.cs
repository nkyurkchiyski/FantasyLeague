using FantasyLeague.Models.Enums;

namespace FantasyLeague.ViewModels.Roster
{
    public class UserRosterViewModel
    {
        public RosterViewModel Roster { get; set; }

        public string ClubName { get; set; }

        public int MarchdayWeek { get; set; }
        
        public int[] RostersWeeks { get; set; }

        public TransferWindowStatus TransferWindowStatus { get; set; }
    }
}
