namespace WebStoreProject.Services
{
    public interface IEmailManger
    {
        void SendEmail(string content, string email, string subject, string path = null);
    }
}
