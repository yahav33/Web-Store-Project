using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    internal class FakeEMailManger : IEmailManger
    {
        public void SendEmail(string content, string email, string subject, string path = null)
        {
            
        }
    }
}
