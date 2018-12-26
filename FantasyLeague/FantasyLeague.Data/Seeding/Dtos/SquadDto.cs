using FantasyLeague.Data.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyLeague.Data.Seeding.Dtos
{
    public class SquadDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("squad")]
        public IList<PlayerDto> Players { get; set; }
    }
}
