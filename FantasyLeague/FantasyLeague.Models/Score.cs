using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class Score : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public Guid FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }

        public double Rating { get; set; }

    }
}
