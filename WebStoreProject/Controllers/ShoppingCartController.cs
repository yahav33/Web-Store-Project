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
    public class ShoppingCartController : Controller
    {

       
        ICartManager _cartManager;
        IRepositoryProducts _repositoryProducts;
        IReadFromBrowser _read;
        IWriteToBrowser _write;
        ICheckUserExist _check;
        ILogger _logger;

        public ShoppingCartController(ICartManager cartManager,
            IRepositoryProducts repositoryProducts, IReadFromBrowser read,
            IWriteToBrowser write , ICheckUserExist checkUser
            ,ILogger logger)
        {
            _cartManager = cartManager;
            _repositoryProducts = repositoryProducts;
            _read = read;
            _write = write;
            _check = checkUser;
            _logger = logger;

        }
        public IActionResult Index()
        {
            
            List<Product> products = new List<Product>();
            string cart = _read.ReadCookie("Cart");
            if(cart == null) return View(products);
            ShoppingCart shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
            var productListIDs = _cartManager.GetCartProducts(shoppingCart);
            var productList = _repositoryProducts.GetCartProducts(productListIDs);
            
            return View(productList);
        }
        public IActionResult AddToCart(long productID)
        {
           
            string cart = _read.ReadCookie("Cart");
            string currentUser = _read.ReadSession("User");
            ShoppingCart userCart = null;
            if (currentUser == null) // No user login

            { 
                if (cart == null) // No Cart Exist
                {
                    //Create New Cart with product ID
                    userCart = _cartManager.AddProduct(productID, null);
                    _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productID);
                    return RedirectToAction("Index");
                }
                else // Cart exist, add product only
                {
                    userCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
                    userCart = _cartManager.AddProduct(productID, userCart);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productID);
                    return RedirectToAction("Index");
                }

                
            }
            else // there is a User Login in the web store
            {

                if (cart == null)// empty cart to the user login
                {

                    long userID = JsonConvert.DeserializeObject<User>(currentUser).UserId;
                    userCart = _cartManager.AddProduct(productID, null, userID);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productID);
                    return RedirectToAction("Index");

                }
                else // cart with product already
                {
                    userCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
                    userCart = _cartManager.AddProduct(productID, userCart);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productID);
                    return RedirectToAction("Index");
                }
            }

          
        }
        public IActionResult Purchase(string ShoppingCart)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            _repositoryProducts.BuyProducts(ShoppingCart);
            _logger.WriteLog($"Products that Purchase{ShoppingCart}", Catgory.Product);
            return RedirectToAction("index", "Home");
        }
        private void SaveInCart( long productID)
        {
            Product product = _repositoryProducts.GetProduct(productID);
            product.Status = StatusState.In_Cart;
            product.TimeStamp = DateTime.Now;
            _repositoryProducts.SaveProducts();
        }

        public IActionResult RemoveFromCart(long ProductID)
        {
            ShoppingCart userCart = JsonConvert.DeserializeObject<ShoppingCart>(_read.ReadCookie("Cart"));
            if (userCart == null) return View();
            userCart = _cartManager.RemoveProduct(ProductID, userCart);
            _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
            Product product = _repositoryProducts.GetProduct(ProductID);
            product.Status = StatusState.In_Stock;
            _repositoryProducts.SaveProducts();
            return RedirectToAction("Index");
        }
    }
}