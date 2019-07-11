using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryProducts _repositoryProducts;
        private readonly IRepositoryUser _repositoryUser;
        private readonly IReadFromBrowser _read;
        private readonly ICheckUserExist _check;
        private readonly ILogger _logger;

        public ProductController(IRepositoryProducts repositoryProducts, 
            IRepositoryUser user,IReadFromBrowser read,
            ICheckUserExist checkUser, ILogger logger)
        {
            _repositoryProducts = repositoryProducts;
            _repositoryUser = user;
            _read = read;
            _check = checkUser;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string User = _read.ReadSession("User");
            if (User != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            _repositoryProducts.RemoveProduct(id);
            _logger.WriteLog($"Product: {id}", Catgory.Product);
           
            return View();
        }
       
        public IActionResult Details(int prodId)
        {
            var prod = _repositoryProducts.GetProduct(prodId);
            var user = _repositoryUser.GetUser((int)prod.UserId);
            ViewData["userEmail"] = user.Email;
            ViewData["userBirthDate"] = user.BirthDate.ToShortDateString();
            ViewData["userName"] = user.FirstName;
            return View(prod);
        }

        public IActionResult MyProducts()
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            User user = JsonConvert.DeserializeObject<User>(_read.ReadCookie("User"));
            return View(_repositoryProducts.GetProductsOfUser(user.UserId));
        }

        //adding product to the store
        [HttpPost]
        public async Task<IActionResult> AddProductToStore(Product product, IFormFile pic1, IFormFile pic2, IFormFile pic3)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            using (var memorystream = new MemoryStream())
            {
                if (pic1 != null)
                {
                    await pic1.CopyToAsync(memorystream);
                    product.ImageOne = memorystream.ToArray();
                }
            }
            using (var memorystream = new MemoryStream())
            {
                if (pic2 != null)
                {
                    await pic2.CopyToAsync(memorystream);
                    product.ImageTwo = memorystream.ToArray();
                }
            }
            using (var memorystream = new MemoryStream())
            {
                if (pic3 != null)
                {
                    await pic3.CopyToAsync(memorystream);
                    product.ImageThree = memorystream.ToArray();
                }
            }

            string User = _read.ReadSession("User");
            if (User != null)
            {
                User userP = JsonConvert.DeserializeObject<User>(User);
                product.UserId = userP.UserId;
            }
            _repositoryProducts.AddProduct(product);
            _logger.WriteLog($"Product {product.ProductId} was added By :{product.UserId}", Catgory.Product);
            
            return RedirectToAction("index","home");

        }
    }
}
