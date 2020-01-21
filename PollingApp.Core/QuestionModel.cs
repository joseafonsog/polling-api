using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace PollingApp.Core
{
    public class QuestionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("question")]
        public string Question { get; set; }

        [BsonElement("image_url")]
        public string ImageUrl { get; set; }

        [BsonElement("thumb_url")]
        public string ThumbUrl { get; set; }

        [BsonElement("published_at")]
        public DateTime PublishedAt { get; set; }

        [BsonElement("choices")]
        public List<ChoiceModel> Choices { get; set; }
    }
}
