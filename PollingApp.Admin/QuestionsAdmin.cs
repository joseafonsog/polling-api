using PollingApp.Core;
using PollingApp.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PollingApp.Admin
{
    public class QuestionsAdmin : IQuestionsAdmin
    {
        private readonly IDbService _db;
        private const string COLLECTION = "Questions";

        public QuestionsAdmin(IDbService db)
        {
            _db = db;
        }

        public List<QuestionModel> GetAll(int limit, int offset, string filter)
        {
            var limitApplied = limit;

            if (limit > 10)
            {
                limitApplied = 10;
            }

            return _db.LoadRecords<QuestionModel>(COLLECTION, limitApplied, offset, filter);
        }

        public QuestionModel GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("The parameter id is required!");

            return _db.LoadRecordById<QuestionModel>(COLLECTION, id);
        }

        public void SaveOrUpdate(QuestionModel question) => SaveOrUpdate(question, null);

        public void SaveOrUpdate(QuestionModel question, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _db.InsertRecord(COLLECTION, question);
                return;
            }

            _db.UpsertRecord(COLLECTION, id, question);
        }
    }
}
