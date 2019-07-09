using Newtonsoft.Json;
using System.Linq;
using WebStoreProject.Models;

namespace WebStoreProject.Services
{
    public class EmptyCart : IEmptyCart
    {
        private readonly ICartManager _cartManager;
        private readonly IRepositoryProducts _repositoryProducts;

        public EmptyCart(ICartManager cartManager,IRepositoryProducts repositoryProducts)
        {
            _cartManager = cartManager;
            _repositoryProducts = repositoryProducts;
        }

        //TODO: This implementation looks very strange... Does this work?
        void IEmptyCart.EmptyCart(string cart)
        {
            if (cart == null) return;
            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(cart);
            var ids = shoppingCart.ProductIDs;
            var count = shoppingCart.ProductIDs.Count;

            for (var i = 0; i < count; i++)
            {
                var tmp = _repositoryProducts.GetProduct(ids.Last());
                tmp.Status = StatusState.InStock;
                _cartManager.RemoveProduct(ids.Last(), shoppingCart);
            }

            _repositoryProducts.SaveProducts();
        }
    }
}
