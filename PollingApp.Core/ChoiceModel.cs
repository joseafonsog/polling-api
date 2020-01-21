using MongoDB.Bson.Serialization.Attributes;

namespace PollingApp.Core
{
    public class ChoiceModel
    {
        [BsonElement("choice")]
        public string Choice { get; set; }

        [BsonElement("votes")]
        public int Votes { get; set; }
    }
}
