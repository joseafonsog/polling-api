using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PollingApp.Api.Dtos
{
    public class QuestionDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("choices")]
        public List<ChoiceDto> Choices { get; set; }
    }
}
