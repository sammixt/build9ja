using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class AppRedisSpecification : BaseSpecification<AppRedis>
    {
        public AppRedisSpecification()
        {
        }

        public AppRedisSpecification(string key)
            : base(x => x.Id.Trim() == key)
        {

        }
    }
}