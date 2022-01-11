using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class VendorBankInfo : BaseEntity
    {
        public string SellerId { get; set;}
        public string BankName {get; set;}
        public string AccountNumber {get; set;}
        public string AccountName {get; set;}
    }
}