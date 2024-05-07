using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class CustomerBasketDto
    {
         public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        public int? DeliveryMethod { get; set; }

        public string PaymentIntent { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}