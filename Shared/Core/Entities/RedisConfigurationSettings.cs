using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RedisConfigurationSettings
    {
        public string ConnectionString { get; set; }
        public int IdentityRedisDb { get; set; }
    }
}
