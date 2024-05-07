using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class DeliveryMethod: BaseEntity
    {
        public DeliveryMethod()
        {
        }

        public string State { get; set; }

        public string LocalGovt { get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }

        public string ShippingId {get; set;}
    }
}