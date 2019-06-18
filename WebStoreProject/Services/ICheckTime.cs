using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public interface ICheckTime
    {
        bool DateValidation(DateTime register);
        string GetMessage();
    }
}
