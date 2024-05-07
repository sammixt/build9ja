using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class AppRedis : BaseEntity
    {
        public AppRedis()
        {
        }

        
        public new string Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset Expiry { get; set; }
    }
}