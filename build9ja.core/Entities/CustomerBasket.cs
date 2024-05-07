using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public int? DeliveryMethod { get; set; }

        public string PaymentIntent { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}