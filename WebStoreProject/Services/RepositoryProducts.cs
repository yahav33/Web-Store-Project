//using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStoreProject.Data;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class RepositoryProducts : IRepositoryProducts
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailManger _emailManger;
        private readonly IReadFromBrowser _readFromBrowser;
        public RepositoryProducts(StoreContext context,IReadFromBrowser readFromBrowser,IEmailManger emailManger,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailManger = emailManger;
            _readFromBrowser = readFromBrowser;
        }

        public List<Product> GetAllProducts()
        {
          
            
            CheckProductsOverTime();

            return _context.Products.Where(x => x.Status == StatusState.InStock).ToList();
        }
        
        private void CheckProductsOverTime()
        {
            var inCartProducts = _context.Products.Where(x => x.Status == StatusState.InCart).ToList();
            if (inCartProducts != null || inCartProducts.Count > 0)
            {
                foreach (var item in inCartProducts)
                {
                    if (item.TimeStamp.AddMinutes(double.Parse(_configuration["RefreshDuration"])) < DateTime.Now)
                    {
                        item.Status = StatusState.InStock;
                        SaveProducts();
                    }
                }
            }
        }
        private List<Product> CheckProductsOverTimeOflist(List<Product> inCartProducts)
        {
            
            return inCartProducts.Where(x => x.TimeStamp.AddMinutes(double.Parse(_configuration["RefreshDuration"])) > DateTime.Now).ToList();
        }

        public List<Product> GetProductsOfUser(long userId)
        {
            var products = _context.Products.Where(x => x.UserId == (int)userId);
            return products.ToList();
        }
        public List<Product> GetCartProducts(List<long> productids)
        {
            var products = new List<Product>();
            foreach (var prodId in productids)
            {
                products.Add(_context.Products.FirstOrDefault(x => x.ProductId == prodId));
            }
            var a = CheckProductsOverTimeOflist(products);
            products = a;
            return products;
        }
        public bool CanBuyAllProducts(List<Product> userProducts)
        {    
            foreach (var item in userProducts)
            {
                if (item.Status != StatusState.InCart)
                    return false;
            }
            return true;

        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public decimal TotalCardWorth(string shoppingCart)
        {
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
            var list = GetCartProducts(cart.ProductIDs);
            decimal totalAmount = 0;
            list.ForEach(x => totalAmount += x.Price);
            return totalAmount;
        }

        public decimal TotalCardWorthForMembers(string shoppingCart)
        {
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
            var list = GetCartProducts(cart.ProductIDs);
            decimal totalAmount = 0;
            foreach (var item in list)
            {
                var price = Convert.ToDouble(item.Price) * 0.9;
                totalAmount += decimal.Parse(price.ToString());
            }
            return totalAmount;
        }
        public Product GetProduct(long id)
        {
            
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }
        public int SaveProducts()
        {
            return _context.SaveChanges();
        }

        public void BuyProducts(string shoppingCart)
        {
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
            var list = GetCartProducts(cart.ProductIDs);
            var wrongItemList = CheckWrongList(list);
            if (wrongItemList.Count != 0)
            {
                //Show the user an error with him list of the wrong items

            }
            else
            {
                EmailForList(list);
                list.ForEach(x => Purchase(x));
                SaveProducts();
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("Cart");
            }
        }

        private void EmailForList(List<Product> list)
        {
            var user = _readFromBrowser.ReadCookie("User");
            if (user == null) return;
            var thisUser = JsonConvert.DeserializeObject<User>(user);
            var message = "";
            foreach (var prod in list)
            {
                message += $"<h4>Product : {prod.Title} with the price of : {(double)prod.Price * 0.9} was purchased</h4>"; 
            }
            _emailManger.SendEmail(message, thisUser.Email, "Purchased Notification!");
        }

        private void Purchase(Product x)
        {
            x.Status = StatusState.Purchased;
            var userMan = _httpContextAccessor.HttpContext.Session.GetString("User");
            
            if (userMan == null)
                x.OwnerId = 1;
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userMan);
                x.OwnerId = user.UserId;
            }
           
        }

        private List<Product> CheckWrongList(List<Product> list)
        {
            return list.Where(x => x.Status != StatusState.InCart).ToList();
        }

        public void RemoveProduct(long id)
        {

            _context.Products.Remove(_context.Products.FirstOrDefault(x => x.ProductId == id));
            _context.SaveChanges();
        }

        public List<Product> Search(string searchKey)
        {
            return _context.Products.Where(x => x.Title.Contains(searchKey)).ToList();
        }
    }
}
