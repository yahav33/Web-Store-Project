﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
    public class ReadPhoto : IReadPhoto
    {
        public string ByteToImage(byte[] arr)
        {
            var base64 = Convert.ToBase64String(arr);
            return string.Format("data:image/gif;base64,{0}", base64);
        }
    }
}
