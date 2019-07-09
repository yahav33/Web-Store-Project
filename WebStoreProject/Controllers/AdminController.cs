using Microsoft.AspNetCore.Mvc;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly ICheckUserExist _check;
        private readonly ILogger _logger;
        private readonly IReadFromBrowser _read;

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

        public IActionResult EditUser(long userId)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            var user = _repositoryUser.GetUser((int)userId);
            if (user != null)
                return View(user);

            return RedirectToAction("Index", "Home");
        }


        [HttpPut]
        public IActionResult AuthorizationAdmin([FromBody]UpdateUser user)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            if (user.IsChecked == "yes")
            {
                var tempUser = _repositoryUser.GetUser((int)user.Userid);
                if (tempUser != null)
                {
                    tempUser.Level = 1;
                    _logger.WriteLog($"User : {tempUser.UserName} , Updated To Admin Level", Catgory.User);
                    _repositoryUser.SaveUsers();
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var tempUser = _repositoryUser.GetUser((int)user.Userid);
                if (tempUser != null)
                {
                    tempUser.Level = 0;
                    _repositoryUser.SaveUsers();
                }
            }

            //TODO: Should the below not be RedirectToAction("index", "Admin"); ?
            return View("index", "Admin");
        }
    }
}