using FantasyLeague.Models.Abstract;
using FantasyLeague.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models
{
    public class Transfer : BaseEntity
    {
        public Guid RosterId { get; set; }
        public virtual Roster Roster { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public Guid MatchdayId { get; set; }
        public virtual Matchday Matchday { get; set; }

        public TransferType TransferType { get; set; }

    }
}
