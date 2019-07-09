namespace WebStoreProject.Services
{
    public interface IWriteToBrowser
    {
       void WriteCookies(string key, string value);
       void DeleteCookies(string name);
       void WriteToSession(string key, string value);
       void DeleteSession(string name);
    }
}
