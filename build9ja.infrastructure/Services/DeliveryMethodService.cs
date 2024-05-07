using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{

    public class DeliveryMethodService : IDeliveryMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(DeliveryMethod deliveryMethod)
        {
            _unitOfWork.Repository<DeliveryMethod>().Add(deliveryMethod);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<List<DeliveryMethod>> getDeliveryMehtodDataTable(DataTableRequestSpecification spec)
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification(spec);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().ListAsync(specification);
            return (List<DeliveryMethod>)deliveryMethod;
        }

         public async Task<int> getCount()
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification();
            var brands = await _unitOfWork.Repository<DeliveryMethod>().CountAsync(specification);
            return brands;
        }

        public async Task<DeliveryMethod> getDeliveryMethodById(string id)
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification(id);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetEntityWithSpec(specification);
            return deliveryMethod;
        }

        public async Task<DeliveryMethod> getDeliveryMethodById(long id)
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification(id);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetEntityWithSpec(specification);
            return deliveryMethod;
        }

        public async Task<DeliveryMethod> getDeliveryByStateAndLga(string state = null, string lga = null)
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification(state, lga);
            DeliveryMethodSpecification specification2 = new DeliveryMethodSpecification(state, null);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetEntityWithSpec(specification);
            if(deliveryMethod is null)
                deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetEntityWithSpec(specification2);
            return deliveryMethod;
        }

        public async Task<int> Update(DeliveryMethod deliveryMethod)
        {
            DeliveryMethodSpecification specification = new DeliveryMethodSpecification(deliveryMethod.ShippingId);
            var entity = await _unitOfWork.Repository<DeliveryMethod>().GetEntityWithSpec(specification);
            if (entity == null) return 404;
            entity.State = deliveryMethod.State ?? entity.State;
            entity.LocalGovt = deliveryMethod.LocalGovt ?? entity.LocalGovt;
            entity.Status = deliveryMethod.Status ?? entity.Status;
            entity.Price = deliveryMethod.Price == 0 ? entity.Price : deliveryMethod.Price;
            _unitOfWork.Repository<DeliveryMethod>().Update(entity);
            int created = await _unitOfWork.Complete();
            return created;
        }

    }
}