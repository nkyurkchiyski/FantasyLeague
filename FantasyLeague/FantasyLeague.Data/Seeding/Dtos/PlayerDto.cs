using Newtonsoft.Json;

namespace FantasyLeague.Data.Dtos
{
    public class PlayerDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("shirtNumber")]
        public int? ShirtNumber { get; set; }
    }
}
