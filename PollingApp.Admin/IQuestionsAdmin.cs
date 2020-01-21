using System.Collections.Generic;
using PollingApp.Core;

namespace PollingApp.Admin
{
    public interface IQuestionsAdmin
    {
        List<QuestionModel> GetAll(int limit, int offset, string filter);
        QuestionModel GetById(string id);
        void SaveOrUpdate(QuestionModel question);
        void SaveOrUpdate(QuestionModel question, string id);
    }
}