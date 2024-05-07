using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface IBrandService
    {
        Task<int> CreateBrand(Brand brand);
        Task<Brand> GetBrandById(long id);
        Task<Brand> GetBrandByName(string name);
        Task<List<Brand>> GetBrands();
        Task<List<Brand>> getBrandDataTable(DataTableRequestSpecification spec);
        Task<int> getCount();
        Task<string> UpdateBrand(Brand brand);
    }
}