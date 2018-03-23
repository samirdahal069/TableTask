﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TableTask.Helpers
{
    public static class ImageConverter
    {

        public static byte[] StreamToByteArray(this Stream input)
        {
            input.Position = 0;
            using (var ms = new MemoryStream())
            {
                int length = System.Convert.ToInt32(input.Length);
                input.CopyTo(ms, length);
                return ms.ToArray();
            }
        }
    }
}