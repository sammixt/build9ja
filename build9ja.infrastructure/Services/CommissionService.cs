using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{

    public class CommissionService : ICommissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateCommission(Commission commission)
        {
            _unitOfWork.Repository<Commission>().Add(commission);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<List<Commission>> GetCommissions()
        {
            CommissionSpecification spec = new CommissionSpecification();
            var commission = await _unitOfWork.Repository<Commission>().ListAsync(spec);
            return (List<Commission>)commission;
        }

        public async Task<Commission> GetCommissionsById(long id)
        {
            CommissionSpecification spec = new CommissionSpecification(id);
            var commission = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            return commission;
        }

        public async Task<Commission> GetCommissionsByName(string commissionName)
        {
            CommissionSpecification spec = new CommissionSpecification(commissionName);
            var commission = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            return commission;
        }

        public async Task<int> UpdateCommission(Commission commission)
        {
            CommissionSpecification spec = new CommissionSpecification(commission.Id);
            var commissions = await _unitOfWork.Repository<Commission>().GetEntityWithSpec(spec);
            if (commissions != null)
            {
                commissions.CommissionType = commission.CommissionType ?? commissions.CommissionType;
                commissions.CommissionPercentage = commission.CommissionPercentage;
                commissions.Status = commission.Status;
                _unitOfWork.Repository<Commission>().Update(commissions);
                int created = await _unitOfWork.Complete();
                return created;
            }

            return 0;
        }
    }
}