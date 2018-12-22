using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class Score : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }

        public Guid FixtureId { get; set; }
        public Fixture Fixture { get; set; }

        public double Rating { get; set; }

    }
}
