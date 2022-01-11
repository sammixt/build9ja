using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class Vendor : BaseEntity
    {
        public string SellerId {get; set;}
        public string Company {get; set;}
        public string TaxNumber {get; set;}
        public string CacNumber {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
        public string Description {get; set;}
        public string Address {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string Status {get; set;}
       
    }
}