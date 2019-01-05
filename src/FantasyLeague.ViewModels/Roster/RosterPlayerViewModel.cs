using FantasyLeague.ViewModels.Score;
using System;

namespace FantasyLeague.ViewModels.Roster
{
    public class RosterPlayerViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int CurrentPoints { get; set; }
        
        public bool IsSub { get; set; }
    }
}
