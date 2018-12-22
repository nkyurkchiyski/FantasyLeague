using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class RosterPlayer : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public Guid RosterId { get; set; }
        public Roster Roster { get; set; }
    }
}