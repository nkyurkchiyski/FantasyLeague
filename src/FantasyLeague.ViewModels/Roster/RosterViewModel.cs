using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Matchday;
using System;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Roster
{
    public class RosterViewModel
    {
        public Guid Id { get; set; }

        public MatchdayViewModel Matchday { get; set; }

        public Formation Formation { get; set; }

        public double Budget { get; set; }
        
        public ICollection<RosterPlayerViewModel> Players { get; set; }

        public int Points { get; set; }
    }
}
