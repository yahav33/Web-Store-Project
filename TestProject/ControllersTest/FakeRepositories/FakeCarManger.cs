using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Models;
using WebStoreProject.Services;

namespace TestProject.ControllersTest.FakeRepositories
{
    class FakeCarManger : ICartManager
    {
       
        public ShoppingCart AddProduct(long ProductID, ShoppingCart Cart, long UserID = -1)
        {
           Cart
        }

        public List<long> GetCartProducts(ShoppingCart Cart)
        {
            
        }

        public ShoppingCart RemoveProduct(long ProductID, ShoppingCart Cart)
        {
            
    }
}
