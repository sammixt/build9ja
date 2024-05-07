using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;

namespace build9ja.core.Specifications
{
    public class DeliveryMethodSpecification : BaseSpecification<DeliveryMethod>
    {
        public DeliveryMethodSpecification()
        {
        }

        public DeliveryMethodSpecification(string id)
            : base(x => x.ShippingId.Equals(id))
        {
        }

        public DeliveryMethodSpecification(long id)
            : base(x => x.Id == id)
        {
        }

        public DeliveryMethodSpecification(string state, string lga)
            : base(x => x.State.ToLower().Equals(state.ToLower()) &&
            (string.IsNullOrEmpty(lga) || x.LocalGovt.ToLower().Contains(lga.ToLower())))
        {
        }

         public DeliveryMethodSpecification(DataTableRequestSpecification spec)
		: base(x => (string.IsNullOrEmpty(spec.SearchValue) || x.State.Contains(spec.SearchValue)
		|| x.LocalGovt.Contains(spec.SearchValue))){
			
		}
    }
}