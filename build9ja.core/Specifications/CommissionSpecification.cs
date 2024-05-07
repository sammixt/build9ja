using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class CommissionSpecification : BaseSpecification<Commission>
    {
        public CommissionSpecification(){}

        public CommissionSpecification(long id)
            : base(x => x.Id == id)
        {
            
        }

        public CommissionSpecification(string commissionType)
            : base(x => x.CommissionType.Trim().Equals(commissionType.Trim()))
        {
            
        }
    }
}