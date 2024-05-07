using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;

namespace build9ja.infrastructure.Services
{

    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateBrand(Brand brand)
        {
            _unitOfWork.Repository<Brand>().Add(brand);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<List<Brand>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<Brand>().ListAllAsync();
            return (List<Brand>)brands;
        }

        public async Task<Brand> GetBrandById(long id)
        {
            BrandSpecification spec = new BrandSpecification(id);
            var brand = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(spec);
            return brand;
        }

        public async Task<Brand> GetBrandByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            BrandSpecification spec = new BrandSpecification(name.ToLower());
            var brand = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(spec);
            return brand;
        }

        public async Task<List<Brand>> getBrandDataTable(DataTableRequestSpecification spec)
        {
            BrandSpecification specification = new BrandSpecification(spec);
            var brands = await _unitOfWork.Repository<Brand>().ListAsync(specification);
            return (List<Brand>)brands; ;
        }

        public async Task<int> getCount()
        {
            BrandSpecification specification = new BrandSpecification();
            var brands = await _unitOfWork.Repository<Brand>().CountAsync(specification);
            return brands;
        }

        public async Task<string> UpdateBrand(Brand brand)
        {
            BrandSpecification spec = new BrandSpecification(brand.Id);
            var model = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(spec);
            if (model == null) return "NE";
            model.BrandLogo = brand.BrandLogo ?? model.BrandLogo;
            model.BrandName = brand.BrandName ?? model.BrandName;
            model.IsTopBrand = brand.IsTopBrand;
            _unitOfWork.Repository<Brand>().Update(model);
            int outcome = await _unitOfWork.Complete();
            if (outcome > 0) return "00";
            return "ER";

        }
    }
}