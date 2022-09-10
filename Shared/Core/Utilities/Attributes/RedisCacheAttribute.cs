using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Attributes
{
    public class RedisCacheAttribute:Attribute
    {
        public int Expiration { get; set; } = 300;
        public string CacheKeyPrefix { get; set; } = string.Empty;
        public bool IsHighAvailability { get; set; } = true;
        public bool OnceUpdate { get; set; } = false;
    }
}
