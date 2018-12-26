using System;
using System.Collections.Generic;

namespace FantasyLeague.Data.Seeding.Dtos
{
    public class MatchdayDto
    {
        public int Week { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<FixtureDto> Fixtures { get; set; }
    }
}
