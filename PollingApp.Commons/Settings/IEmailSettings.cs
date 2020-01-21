namespace PollingApp.Commons.Settings
{
    public interface IEmailSettings
    {
        string LocalDirectoryPath { get; set; }
        string Sender { get; set; }
        string Subject { get; set; }
    }
}