using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.client.Dto
{
    public class CheckoutShippingAddress : AddressDto
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public decimal SubTotal {get; set;}

        public decimal ShippingCost {get; set;}

       public CustomerBasketDto Basket {get; set;}
    }
}