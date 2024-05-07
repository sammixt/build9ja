using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface IDeliveryMethodService
    {
        Task<int> Create(DeliveryMethod deliveryMethod);
        Task<List<DeliveryMethod>> getDeliveryMehtodDataTable(DataTableRequestSpecification spec);
        Task<DeliveryMethod> getDeliveryByStateAndLga(string state = null, string lga = null);
        Task<DeliveryMethod> getDeliveryMethodById(string id);
        Task<DeliveryMethod> getDeliveryMethodById(long id);
        Task<int> Update(DeliveryMethod deliveryMethod);
        Task<int> getCount();
    }
}