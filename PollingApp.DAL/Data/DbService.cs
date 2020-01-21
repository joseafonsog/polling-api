using MongoDB.Bson;
using MongoDB.Driver;
using PollingApp.Commons.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollingApp.DAL.Data
{
    public class DbService : IDbService
    {
        private readonly IMongoDatabase _db;

        public DbService(IQuestionsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _db = client.GetDatabase(settings.DatabaseName);
        }

        public void InsertRecord<T>(string collection, T record) where T : class
        {
            var col = _db.GetCollection<T>(collection);
            col.InsertOne(record);
        }

        public void InsertManyRecords<T>(string collection, IList<T> records) where T : class
        {
            var col = _db.GetCollection<T>(collection);
            col.InsertMany(records);
        }

        public List<T> LoadRecords<T>(string collection, int limit, int offset, string filter) where T : class
        {
            var col = _db.GetCollection<T>(collection);

            FilterDefinition<T> filters = new BsonDocument();
            if (!string.IsNullOrEmpty(filter))
            {
                var questionFilter = new BsonDocument("question", new BsonRegularExpression(filter, "im"));
                var choicesFilter = new BsonDocument("choices.choice", new BsonRegularExpression(filter, "im"));
                filters = new BsonDocument("$or", new BsonArray { questionFilter, choicesFilter });
            }
                
            return col.Find(filters)
                .Skip(offset)
                .Limit(limit)
                .ToList();
        }

        public T LoadRecordById<T>(string collection, string id) where T : class
        {
            var col = _db.GetCollection<T>(collection);
            var filter = new BsonDocument("_id", new ObjectId(id));
            return col
                .Find(filter)
                .First();
        }

        public void UpsertRecord<T>(string collection, string id, T record) where T : class
        {
            var col = _db.GetCollection<T>(collection);
            var filter = new BsonDocument("_id", new ObjectId(id));
            col.ReplaceOne(filter, record, new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string collection, string id) where T : class
        {
            var col = _db.GetCollection<T>(collection);
            var filter = new BsonDocument("_id", new ObjectId(id));
            col.DeleteOne(filter);
        }
    }
}
