using MonkeyCache.FileStore;
using PRMOVIL2CARWASH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRMOVIL2CARWASH.Utils
{
    public class Cache
    {
        public static void SaveCache(string key, User user, int expireIn)
        {
            if (Barrel.Current.Exists(key: key))
            {
                Barrel.Current.Empty(key: key);
            }
            Barrel.Current.Add<User>(key: key, data: user, expireIn: TimeSpan.FromDays(expireIn));
        }
    }
}
