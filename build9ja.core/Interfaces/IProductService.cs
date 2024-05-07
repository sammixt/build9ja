using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface IProductService
    {
        //Get product by Id
        //Get Paginated Product by Brand
        Task<List<Product>>  GetProductByBrand(PaginationSpecification spec);
        //Get Paginated Product by Category
        Task<List<Product>>  GetProductByCategory(PaginationSpecification spec);
        Task<List<Product>> GetProductByCategory(string category,PaginationSpecification spec);
        Task<List<Product>> GetProductByCategory(long categoryId);
        //Get Paginated Product
        Task<List<Product>>  GetProducts(PaginationSpecification spec);
        //Get Paginated Product by VendorId
        Task<List<Product>>  GetProductByVendor(PaginationSpecification spec);
        Task<int> CreateProduct(Product product);
        Task<int> getCount();
        Task<IReadOnlyList<Product>> getProductDataTable(DataTableRequestSpecification spec);
        Task<Product> GetProductByProductId(string productId);
        Task<string> UpdateProduct(Product product);
        Task<Entities.ProductSpecification> GetProductSpecification(string productIdString);
        Task<int> AddOrUpdateProductSpecification(Entities.ProductSpecification model);
        Task<int> AddOrUpdateProductVariation(ProductVariation model);
        Task<int> AddOrUpdateProductImage(ProductImage model);
        Task<int> UpdateProductStatus(string productId, string status, bool isFeatured);
        Task<List<Product>> GetDealOfWeekProducts(int take);
        Task<List<Product>> GetFeaturedProducts(int take);
        Task<int> GetProductCount(string category,PaginationSpecification spec);
    }
}