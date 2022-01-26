using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Common.Enums
{
    public enum CacheKeyType
    {
        AllRents = 0,
        AllVehicles = 1,
        AllUsers = 2
    }

    public static class CacheKeyTypeExtensions
    {
        public static string AsString(this CacheKeyType cacheKeyType)
        {
            return cacheKeyType switch
            {
                CacheKeyType.AllRents => "AllRents",
                CacheKeyType.AllVehicles => "AllVehicles",
                CacheKeyType.AllUsers => "AllUsers",
                _ => string.Empty
            };
        }
    }
}
