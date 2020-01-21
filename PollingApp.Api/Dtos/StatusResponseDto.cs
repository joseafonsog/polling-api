using Newtonsoft.Json;

namespace PollingApp.Api.Dtos
{
    public class StatusResponseDto
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
