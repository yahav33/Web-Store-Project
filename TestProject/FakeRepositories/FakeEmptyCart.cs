using System;
using System.Collections.Generic;
using System.Text;
using WebStoreProject.Services;

namespace TestProject.FakeRepositories
{
    class FakeEmptyCart : IEmptyCart
    {
        public void EmptyCart(string Cart)
        {
            throw new NotImplementedException();
        }
    }
}
