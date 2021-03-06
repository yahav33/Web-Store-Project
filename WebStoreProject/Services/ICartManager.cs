﻿using System.Collections.Generic;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public interface ICartManager
    {
        
        ShoppingCart AddProduct(long ProductID,ShoppingCart Cart,long UserID = -1);
        ShoppingCart RemoveProduct(long ProductID, ShoppingCart Cart);
        List<long> GetCartProducts(ShoppingCart Cart);
       


    }
}
