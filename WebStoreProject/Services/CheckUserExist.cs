namespace WebStoreProject.Services
{
    public class CheckUserExist : ICheckUserExist
    {
        private readonly IReadFromBrowser _read;

        public CheckUserExist(IReadFromBrowser readFrom)
        {
            _read = readFrom;
        }

        public bool CheckUserLogin()
        {
            if (_read.ReadCookie("User") != null) return true;
            return false;
        }
    }
}
