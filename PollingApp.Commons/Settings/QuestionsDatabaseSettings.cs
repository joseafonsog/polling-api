using System;
using System.Collections.Generic;
using System.Text;

namespace PollingApp.Commons.Settings
{
    public class QuestionsDatabaseSettings : IQuestionsDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
