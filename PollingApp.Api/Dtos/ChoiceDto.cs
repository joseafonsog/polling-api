using Newtonsoft.Json;

namespace PollingApp.Api.Dtos
{
    public class ChoiceDto
    {
        [JsonProperty("choice")]
        public string Choice { get; set; }

        [JsonProperty("votes")]
        public int Votes { get; set; }
    }
}
