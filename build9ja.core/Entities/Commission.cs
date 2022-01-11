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
            DateCreated = DateTime.UtcNow;
        }

        public string CommissionType {get; set;}
        public decimal CommissionPercentage {get; set;}

        public DateTime DateCreated {get; set;}

        public string Status {get; set;}

    }
}