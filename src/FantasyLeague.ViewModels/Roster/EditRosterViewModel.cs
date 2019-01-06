using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.Matchday;
using System;
using System.Collections.Generic;

namespace FantasyLeague.ViewModels.Roster
{
    public class EditRosterViewModel
    {
        public Guid RosterId { get; set; }

        public MatchdayViewModel Matchday { get; set; }

        public Formation Formation { get; set; }

        public virtual IList<RosterPlayerViewModel> Goalkeepers { get; set; }

        public virtual IList<RosterPlayerViewModel> Defenders { get; set; }

        public virtual IList<RosterPlayerViewModel> Midfielders { get; set; }

        public virtual IList<RosterPlayerViewModel> Attackers { get; set; }
    }
}
