using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryProducts _repositoryProducts;
        IReadFromBrowser _read;
        IWriteToBrowser _write;
       

        public HomeController(IRepositoryProducts repositoryProducts, IReadFromBrowser read, IWriteToBrowser write)
        {
            _repositoryProducts = repositoryProducts;
            _read = read;
            _write = write;
          
        }
        public bool UserLogin()
        {
            //can use from services
            return (_read.ReadSession("User") != null);
        }
        [HttpPost]
        public IActionResult Search(string searchinput)
        {
            var v = HttpContext.Request.Form["searchinput"];
            return View("Index",_repositoryProducts.Search(searchinput));

        }
        public IActionResult Index(string sortOrder)     
        {
            
            string user = _read.ReadCookie("User");
            if (user != null)
            {
                //if user exist
                _write.WriteToSession("User", user);
                User CurrectUser = JsonConvert.DeserializeObject<User>(user);
                ViewData["Name"] = CurrectUser.FirstName;
            }

            else
            {
                string UserPP = _read.ReadSession("User");
                if (UserPP != null)
                {
                    User CurrectUser = JsonConvert.DeserializeObject<User>(UserPP);
                    ViewData["Name"] = CurrectUser.FirstName;

                }
            }
            
            var prods = _repositoryProducts.GetAllProducts();
            switch (sortOrder)
            {
                case "name":
                    prods = prods.OrderByDescending(p => p.Title).ToList();
                    break;
                case "date":
                    prods = prods.OrderBy(p => p.Date).ToList();
                    break;
                case "price":
                    prods = prods.OrderBy(p => p.Price).ToList();
                    break;
                default:
                    prods = prods.OrderBy(p => p.Title).ToList();
                    break;
            }
            return View(prods);
        }

        public IActionResult About()
        {
            return View();
        }


    }
}