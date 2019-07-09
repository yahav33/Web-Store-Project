using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    internal class FakeCheckUser : ICheckUserExist
    {
        public bool CheckUserLogin()
        {
            return true;
        }
    }
}
