using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class CartManager : ICartManager
    {
        private IReadFromBrowser _read;

        public CartManager(IReadFromBrowser readFrom)
        {
            _read = readFrom;
        }

        public ShoppingCart AddProduct(long ProductID, ShoppingCart Cart, long UserID = -1)
        {
            if (Cart == null) CreateCart(ref Cart,UserID);
           
            Cart.ProductIDs.Add(ProductID);
            return Cart;
        }

        public List<long> GetCartProducts(ShoppingCart Cart)
        {
            return Cart.ProductIDs;
        }

        public ShoppingCart RemoveProduct(long ProductID, ShoppingCart Cart)
        {
            Cart.ProductIDs.Remove(ProductID);
            return Cart;
        }

        

        private void CreateCart(ref ShoppingCart Cart,long UserID)
        {
            Cart = new ShoppingCart { UserID = UserID };
            Cart.ProductIDs = new List<long>();
            Cart.InitCartTime = DateTime.Now;
        }

      

        
    }
}
