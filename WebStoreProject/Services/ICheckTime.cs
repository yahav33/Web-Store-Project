using System;

namespace WebStoreProject.Services
{
    public interface ICheckTime
    {
        bool DateValidation(DateTime register);
        string GetMessage();
    }
}
