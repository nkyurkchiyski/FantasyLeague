using FantasyLeague.ViewModels.Score;
using System;

namespace FantasyLeague.ViewModels.Roster
{
    public class RosterPlayerViewModel
    {
        public Guid PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string PlayerImage { get; set; }

        public ScoreViewModel Score { get; set; }

        public int Points { get; set; }

        public bool IsSub { get; set; }
    }
}
