namespace PollingApp.Admin
{
    public interface IShareAdmin
    {
        void SendEmail(string to, string content);
    }
}