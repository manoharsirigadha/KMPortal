using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

//using System.Runtime.Caching;   
namespace IFDS.KMPortal.Helper
{
    public static class CacheHelper
    {

        public static void SaveTocache(string cacheKey, object savedItem, DateTime absoluteExpiration)
        {
            if (IsIncache(cacheKey))
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }

            HttpContext.Current.Cache.Add(cacheKey, savedItem, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0), System.Web.Caching.CacheItemPriority.Default, null);
        }

        public static object GetFromCache(string cacheKey) 
        {
            return HttpContext.Current.Cache[cacheKey];
        }

        public static void RemoveFromCache(string cacheKey)
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        public static bool IsIncache(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey] != null;
        }
    }
}
