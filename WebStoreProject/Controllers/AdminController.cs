using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryUser _repositoryUser;
        ICheckUserExist _check;
        ILogger _logger;
        IReadFromBrowser _read;

        public AdminController(IRepositoryUser repositoryUser,
            ICheckUserExist exist, ILogger logger, IReadFromBrowser read)
        {
            _repositoryUser = repositoryUser;
            _check = exist;
            _logger = logger;
            _read = read;

        }
        public IActionResult Index()
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            return View(_repositoryUser.GetUsers());
        }

        public IActionResult EditUser(long UserID)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            User user = _repositoryUser.GetUser((int)UserID);
            if(user != null)
            return View(user);

            return RedirectToAction("Index", "Home");
        }

        
        [HttpPut]
        public IActionResult AuthorizationAdmin([FromBody]UpdateUser user)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            if (user.isChecked == "yes")
            {
                User tempuser = _repositoryUser.GetUser((int)user.Userid);
                if(tempuser != null)
                {
                    tempuser.Level = 1;
                    _logger.WriteLog($"User : {tempuser.UserName} , Updated To Admin Level",Catgory.User);
                    _repositoryUser.SaveUsers();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User tempuser = _repositoryUser.GetUser((int)user.Userid);
                if (tempuser != null)
                {
                    tempuser.Level = 0;
                    _repositoryUser.SaveUsers();
                }
            }
            return View("index","Admin");
        }
    }
}