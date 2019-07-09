using System.Collections.Generic;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public interface ICartManager
    {
        
        ShoppingCart AddProduct(long productId,ShoppingCart cart,long userId = -1);
        ShoppingCart RemoveProduct(long productId, ShoppingCart cart);
        List<long> GetCartProducts(ShoppingCart cart);
       


    }
}
