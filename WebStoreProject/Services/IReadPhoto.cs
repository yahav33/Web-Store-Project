using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
  public interface IReadPhoto
    {
        string ByteToImage(byte[] arr);
    }
}
