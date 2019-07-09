using System;
using System.Collections.Generic;
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

        public ShoppingCart AddProduct(long productId, ShoppingCart cart, long userId = -1)
        {
            if (cart == null) CreateCart(ref cart,userId);
           
            cart.ProductIDs.Add(productId);
            return cart;
        }

        public List<long> GetCartProducts(ShoppingCart cart)
        {
            return cart.ProductIDs;
        }

        public ShoppingCart RemoveProduct(long productId, ShoppingCart cart)
        {
            cart.ProductIDs.Remove(productId);
            return cart;
        }

        

        private void CreateCart(ref ShoppingCart cart,long userId)
        {
            cart = new ShoppingCart { UserId = userId };
            cart.ProductIDs = new List<long>();
            cart.InitCartTime = DateTime.Now;
        }

      

        
    }
}
