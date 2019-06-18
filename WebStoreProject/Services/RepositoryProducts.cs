//using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Data;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class RepositoryProducts : IRepositoryProducts
    {
        StoreContext _context;
        IConfiguration _configuration;
        IHttpContextAccessor _HttpContextAccessor;
        IEmailManger _emailManger;
        IReadFromBrowser _readFromBrowser;
        public RepositoryProducts(StoreContext context,IReadFromBrowser readFromBrowser,IEmailManger emailManger,IConfiguration configuration, IHttpContextAccessor HttpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _HttpContextAccessor = HttpContextAccessor;
            _emailManger = emailManger;
            _readFromBrowser = readFromBrowser;
        }

        public List<Product> GetAllProducts()
        {
          
            
            CheckProductsOverTime();

            return _context.Products.Where(x => x.Status == StatusState.In_Stock).ToList();
        }
        
        private void CheckProductsOverTime()
        {
            List<Product> inCartProducts = _context.Products.Where(x => x.Status == StatusState.In_Cart).ToList();
            if (inCartProducts != null || inCartProducts.Count > 0)
            {
                foreach (var item in inCartProducts)
                {
                    if (item.TimeStamp.AddMinutes(double.Parse(_configuration["RefreshDuration"])) < DateTime.Now)
                    {
                        item.Status = StatusState.In_Stock;
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
            List<Product> products = new List<Product>();
            foreach (var prodID in productids)
            {
                products.Add(_context.Products.FirstOrDefault(x => x.ProductId == prodID));
            }
            var a = CheckProductsOverTimeOflist(products);
            products = a;
            return products;
        }
        public bool CanBuyAllProducts(List<Product> userProducts)
        {    
            foreach (var item in userProducts)
            {
                if (item.Status != StatusState.In_Cart)
                    return false;
            }
            return true;

        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public decimal TotalCardWorth(string ShoppingCart)
        {
            ShoppingCart Cart = JsonConvert.DeserializeObject<ShoppingCart>(ShoppingCart);
            var list = GetCartProducts(Cart.ProductIDs);
            decimal totalAmount = 0;
            list.ForEach(x => totalAmount += x.Price);
            return totalAmount;
        }

        public decimal TotalCardWorthForMembers(string ShoppingCart)
        {
            ShoppingCart Cart = JsonConvert.DeserializeObject<ShoppingCart>(ShoppingCart);
            var list = GetCartProducts(Cart.ProductIDs);
            decimal totalAmount = 0;
            foreach (var item in list)
            {
                double price = Convert.ToDouble(item.Price) * 0.9;
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

        public void BuyProducts(string ShoppingCart)
        {
            ShoppingCart Cart = JsonConvert.DeserializeObject<ShoppingCart>(ShoppingCart);
            var list = GetCartProducts(Cart.ProductIDs);
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
                _HttpContextAccessor.HttpContext.Response.Cookies.Delete("Cart");
            }
        }

        private void EmailForList(List<Product> list)
        {
            string user = _readFromBrowser.ReadCookie("User");
            if (user == null) return;
            User thisUser = JsonConvert.DeserializeObject<User>(user);
            string message = "";
            foreach (var prod in list)
            {
                message += $"Product : {prod.Title} with the price of : {(double)prod.Price * 0.9} was purchased{Environment.NewLine}"; 
            }
            _emailManger.SendEmail(message, thisUser.Email, "Purchased Notification!");
        }

        private void Purchase(Product x)
        {
            x.Status = StatusState.Purchased;
            string userMan = _HttpContextAccessor.HttpContext.Session.GetString("User");
            
            if (userMan == null)
                x.OwnerId = 1;
            else
            {
                User user = JsonConvert.DeserializeObject<User>(userMan);
                x.OwnerId = user.UserId;
            }
           
        }

        private List<Product> CheckWrongList(List<Product> list)
        {
            return list.Where(x => x.Status != StatusState.In_Cart).ToList();
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
