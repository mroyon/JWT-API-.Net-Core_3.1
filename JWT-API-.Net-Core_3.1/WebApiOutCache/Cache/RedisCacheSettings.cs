using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiOutCache.Cache
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }
}
