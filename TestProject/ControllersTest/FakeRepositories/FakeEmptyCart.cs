using System;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    internal class FakeEmptyCart : IEmptyCart
    {
        public void EmptyCart(string cart)
        {
            throw new NotImplementedException();
        }
    }
}
