using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class EmptyCart : IEmptyCart
    {
        ICartManager _cartManager;
        IRepositoryProducts _repositoryProducts;

        public EmptyCart(ICartManager cartManager,IRepositoryProducts repositoryProducts)
        {
            _cartManager = cartManager;
            _repositoryProducts = repositoryProducts;
        }
        void IEmptyCart.EmptyCart(string Cart)
        {
            if (Cart == null) return;
            ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(Cart);
            List<long> ids = cart.ProductIDs;
            int count = cart.ProductIDs.Count;
            for (int i = 0; i < count; i++)
            {
                Product tmp = _repositoryProducts.GetProduct(ids.Last());
                tmp.Status = StatusState.In_Stock;
                _cartManager.RemoveProduct(ids.Last(), cart);

            }
            _repositoryProducts.SaveProducts();

        }
    }
}
