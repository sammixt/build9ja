using build9ja.core.Entities;

namespace build9ja.core.Interfaces
{
    public interface ICommissionService
    {
        Task<int> CreateCommission(Commission commission);
        Task<List<Commission>> GetCommissions();
        Task<Commission> GetCommissionsById(long id);
        Task<Commission> GetCommissionsByName(string commissionName);
        Task<int> UpdateCommission(Commission commission);
    }
}