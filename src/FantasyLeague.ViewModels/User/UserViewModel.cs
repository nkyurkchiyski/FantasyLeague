using FantasyLeague.ViewModels.Roster;

namespace FantasyLeague.ViewModels.User
{
    public class UserViewModel
    {
        public int TotalPoints { get; set; }

        public int CurrentPoints { get; set; }

        public string ClubName { get; set; }

        public RosterViewModel Roster { get; set; }
    }
}
