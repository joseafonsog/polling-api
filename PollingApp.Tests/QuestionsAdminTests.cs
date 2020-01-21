using Moq;
using PollingApp.Admin;
using PollingApp.Core;
using PollingApp.DAL.Data;
using System;
using System.Linq;
using Xunit;

namespace PollingApp.Tests
{
    public class QuestionsAdminTests
    {
        private const string COLLECTION = "Questions";

        [Fact]
        public void GetByIdShouldThrowExceptionOnEmptyId()
        {
            // Setup
            var mock = new Mock<IDbService>();
            mock.Setup(p => p.LoadRecordById<QuestionModel>(COLLECTION, null)).Returns(new QuestionModel());
            var questionsAdmin = new QuestionsAdmin(mock.Object);

            // Assertion
            Assert.Throws<ArgumentNullException>(() => questionsAdmin.GetById(null));
        }

        [Fact]
        public void GetByIdShouldReturnValue()
        {
            // Setup
            var question = DbSeed.GetQuestions().First();
            question.Id = Guid.NewGuid().ToString();
            var mock = new Mock<IDbService>();
            mock.Setup(p => p.LoadRecordById<QuestionModel>(COLLECTION, question.Id)).Returns(question);
            var questionsAdmin = new QuestionsAdmin(mock.Object);

            // Execution
            var result = questionsAdmin.GetById(question.Id);

            // Assertion
            Assert.Equal(question.Id, result.Id);
        }

        [Fact]
        public void SaveOrUpdateGetByIdShouldExcecuteIrsertRecordOnce()
        {
            //Setup
            var question = DbSeed.GetQuestions().First();
            var id = Guid.NewGuid().ToString();

            var mock = new Mock<IDbService>();
            mock.Setup(p => p.InsertRecord(COLLECTION, question));
            mock.Setup(p => p.UpsertRecord(COLLECTION, id, question));
            var questionsAdmin = new QuestionsAdmin(mock.Object);

            // Execution
            questionsAdmin.SaveOrUpdate(question);

            // Assertion
            mock.Verify(x => x.InsertRecord(COLLECTION, question), Times.Once);
            mock.Verify(x => x.UpsertRecord(COLLECTION, id, question), Times.Never);
        }

        [Fact]
        public void SaveOrUpdateShouldExcecuteUpsertRecordOnce()
        {
            //Setup
            var question = DbSeed.GetQuestions().First();
            var id = Guid.NewGuid().ToString();

            var mock = new Mock<IDbService>();
            mock.Setup(p => p.InsertRecord(COLLECTION, question));
            mock.Setup(p => p.UpsertRecord(COLLECTION, id, question));
            var questionsAdmin = new QuestionsAdmin(mock.Object);

            // Execution
            questionsAdmin.SaveOrUpdate(question, id);

            // Assertion
            mock.Verify(x => x.InsertRecord(COLLECTION, question), Times.Never);
            mock.Verify(x => x.UpsertRecord(COLLECTION, id, question), Times.Once);
        }
    }
}
