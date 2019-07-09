using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace WebStoreProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartManager _cartManager;
        private readonly IRepositoryProducts _repositoryProducts;
        private readonly IReadFromBrowser _read;
        private readonly IWriteToBrowser _write;
        private readonly ICheckUserExist _check;
        private readonly ILogger _logger;

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
            var products = new List<Product>();
            var cart = _read.ReadCookie("Cart");
            if(cart == null) return View(products);
            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
            var productListIDs = _cartManager.GetCartProducts(shoppingCart);
            var productList = _repositoryProducts.GetCartProducts(productListIDs);
            
            return View(productList);
        }
        public IActionResult AddToCart(long productId)
        {
            var cart = _read.ReadCookie("Cart");
            var currentUser = _read.ReadSession("User");
            ShoppingCart userCart;
            if (currentUser == null) // No user login
            { 
                if (cart == null) // No Cart Exist
                {
                    //Create New Cart with product ID
                    userCart = _cartManager.AddProduct(productId, null);
                    _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productId);
                    return RedirectToAction("Index");
                }
                else // Cart exist, add product only
                {
                    userCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
                    userCart = _cartManager.AddProduct(productId, userCart);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productId);
                    return RedirectToAction("Index");
                }
            }
            else // there is a User Login in the web store
            {
                if (cart == null)// empty cart to the user login
                {
                    var userId = JsonConvert.DeserializeObject<User>(currentUser).UserId;
                    userCart = _cartManager.AddProduct(productId, null, userId);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productId);
                    return RedirectToAction("Index");

                }
                else // cart with product already
                {
                    userCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
                    userCart = _cartManager.AddProduct(productId, userCart);
                   _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
                    SaveInCart(productId);
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult Purchase(string shoppingCart)
        {
            if (_check.CheckUserLogin() == false) return RedirectToAction("Index", "Login");
            _repositoryProducts.BuyProducts(shoppingCart);
            _logger.WriteLog($"Products that Purchase{shoppingCart}", Catgory.Product);
            return RedirectToAction("index", "Home");
        }
        private void SaveInCart( long productId)
        {
            var product = _repositoryProducts.GetProduct(productId);
            product.Status = StatusState.InCart;
            product.TimeStamp = DateTime.Now;
            _repositoryProducts.SaveProducts();
        }

        public IActionResult RemoveFromCart(long productId)
        {
            var userCart = JsonConvert.DeserializeObject<ShoppingCart>(_read.ReadCookie("Cart"));

            if (userCart == null) return View();

            userCart = _cartManager.RemoveProduct(productId, userCart);
            _write.WriteCookies("Cart", JsonConvert.SerializeObject(userCart));
            var product = _repositoryProducts.GetProduct(productId);
            product.Status = StatusState.InStock;
            _repositoryProducts.SaveProducts();
            return RedirectToAction("Index");
        }
    }
}