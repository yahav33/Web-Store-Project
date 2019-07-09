namespace WebStoreProject.Services
{
    public interface IReadFromBrowser
    {
        string ReadSession(string name);
        string ReadCookie(string name);
    }
}
