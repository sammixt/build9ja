using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.api.Dto
{
    public class VendorBankInfoDto
    {
        public long Id {get; set;}
        [Required(ErrorMessage="Seller Id is required")]
        [MaxLength(20, ErrorMessage ="Seller ID should not exceed 20 characters")]
        public string SellerId { get; set;}
        [Required(ErrorMessage="Bank name is required")]
        [MaxLength(150, ErrorMessage ="Bank name should not exceed 150 characters")]
        public string BankName {get; set;}
        [Required(ErrorMessage="Account Number is required")]
        [MaxLength(15, ErrorMessage ="Account Number should not exceed 15 characters")]
        public string AccountNumber {get; set;}
        
        [Required(ErrorMessage="Account Name is required")]
        [MaxLength(100, ErrorMessage ="Account Name should not exceed 100 characters")]
        public string AccountName {get; set;}
    }
}