using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace build9ja.core.Entities
{
    public class Commission : BaseEntity
    {
        public Commission()
        {
            
        }

        public string CommissionType {get; set;}
        public decimal CommissionPercentage {get; set;}

        public string Status {get; set;}

    }
}