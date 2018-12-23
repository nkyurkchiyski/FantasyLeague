using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class RosterPlayer : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public Guid RosterId { get; set; }
        public virtual Roster Roster { get; set; }
    }
}