namespace PollingApp.Commons.Settings
{
    public interface IQuestionsDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
