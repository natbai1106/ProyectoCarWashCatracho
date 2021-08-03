using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Utils
{
     public class Constanst
    {
        private const string UrlBase = "http://173.249.21.6/v1";
        public const int  REQUEST_OK= 201;
        public const int REQUEST_ERROR= 400;
        public static string GetUrl(String route)
        {
           return string.Concat(UrlBase, route);
           
        }
    }
}
