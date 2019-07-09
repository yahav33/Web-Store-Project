using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryProducts _repositoryProducts;
        private readonly IReadFromBrowser _read;
        private readonly IWriteToBrowser _write;


        public HomeController(IRepositoryProducts repositoryProducts, IReadFromBrowser read, IWriteToBrowser write)
        {
            _repositoryProducts = repositoryProducts;
            _read = read;
            _write = write;

        }

        [HttpPost]
        public IActionResult Search(string searchInput)
        {
            //var v = HttpContext.Request.Form["searchinput"];
            return View("Index", _repositoryProducts.Search(searchInput));

        }
        public IActionResult Index(string sortOrder)
        {
            //Option 1:
            //string user = _read.ReadCookie("User");
            //if (user != null)
            //{
            //    //if user exist
            //    _write.WriteToSession("User", user);
            //    //TODO: Did you mean to name the argument below currentUser or correctUser? Either way, currectUser is a mistake.
            //    var currectUser = JsonConvert.DeserializeObject<User>(user);
            //    ViewData["Name"] = currectUser.FirstName;
            //}

            //else
            //{
            //    var userPp = _read.ReadSession("User");
            //    if (userPp != null)
            //    {
            //        //TODO: Did you mean to name the argument below currentUser or correctUser? Either way, currectUser is a mistake.
            //        var currectUser = JsonConvert.DeserializeObject<User>(userPp);
            //        ViewData["Name"] = currectUser.FirstName;
            //    }
            //}

            //Option 2:
            //ViewData["Name"] = GetUsersFirstName();

            //Option 3:
            if(TryGetUser(out var currentUser))
                ViewData["Name"] = currentUser.FirstName;

            var products = GetFilteredProducts(sortOrder);
            return View(products);
        }

        private string GetUsersFirstName()
        {
            //Option 2:

            //var user = _read.ReadCookie("User");

            //if (!string.IsNullOrWhiteSpace(user))
            //    _write.WriteToSession("User", user);
            //else
            //    user = _read.ReadSession("User");

            //var currectUser = !string.IsNullOrWhiteSpace(user)
            //    ? JsonConvert.DeserializeObject<User>(user)
            //    : null;

            //return currectUser?.FirstName;

            // Option 3:
            return TryGetUser(out var currentUser) ? currentUser.FirstName : null;
        }

        private bool TryGetUser(out User currentUser)
        {
            var user = _read.ReadCookie("User");

            if (!string.IsNullOrWhiteSpace(user))
                _write.WriteToSession("User", user);
            else
                user = _read.ReadSession("User");

            currentUser = !string.IsNullOrWhiteSpace(user)
                ? JsonConvert.DeserializeObject<User>(user)
                : null;

            return currentUser != null;
        }

        private List<Product> GetFilteredProducts(string sortOrder)
        {
            var products = _repositoryProducts.GetAllProducts();
            switch (sortOrder)
            {
                case "name":
                    products = products.OrderByDescending(p => p.Title).ToList();
                    break;
                case "date":
                    products = products.OrderBy(p => p.Date).ToList();
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Title).ToList();
                    break;
            }

            return products;
        }


        public IActionResult About()
        {
            return View();
        }

        public bool IsUserLoggedIn()
        {
            //can use from services
            return (_read.ReadSession("User") != null);
        }


    }
}