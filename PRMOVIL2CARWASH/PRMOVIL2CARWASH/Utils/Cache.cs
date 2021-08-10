using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PRMOVIL2CARWASH.Utils
{
    public class Cache
    {
        public static void SaveCache(string key, User user, int expireIn)
        {
            Barrel.Current.Add<User>(key: key, data: user, expireIn: TimeSpan.FromDays(expireIn));
        }

        public static void SaveCache(string key, Stream img, int expireIn)
        {
            Barrel.Current.Add<Stream>(key: key, data: img, expireIn: TimeSpan.FromDays(expireIn));
        }
    }
}
