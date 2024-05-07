using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using productSpec = build9ja.core.Specifications;
using ProductSpecification = build9ja.core.Entities.ProductSpecification;

namespace build9ja.infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateProduct(Product product)
        {
            _unitOfWork.Repository<Product>().Add(product);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> AddOrUpdateProductSpecification(ProductSpecification model){
            ProductSpecificationsSpecification spec = new ProductSpecificationsSpecification(model.ProductId,true);
            var productSpecs = await _unitOfWork.Repository<ProductSpecification>().GetEntityWithSpec(spec);
            if(productSpecs == null){
                _unitOfWork.Repository<ProductSpecification>().Add(model);
            }else{
                productSpecs.ColorFamily = model.ColorFamily ?? productSpecs.ColorFamily;
                productSpecs.Dimension = model.Dimension ?? productSpecs.Dimension;
                productSpecs.MainMaterial = model.MainMaterial ?? productSpecs.MainMaterial;
                productSpecs.Model = model.Model ?? productSpecs.Model;
                productSpecs.ProductType = model.ProductType ?? productSpecs.ProductType;
                productSpecs.Weight = model.Weight;
                _unitOfWork.Repository<ProductSpecification>().Update(productSpecs);
            }
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> UpdateProductStatus(string productId, string status, bool isFeatured){
             productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(productId.ToLower().Trim());
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(pSpec);
            if(product == null) return 404;
            product.Status = status ?? product.Status;
            product.IsFeatured = isFeatured;
              _unitOfWork.Repository<Product>().Update(product);
            return await _unitOfWork.Complete();
        }

        public async Task<int> AddOrUpdateProductVariation(ProductVariation model){
            
            if(model.Id == 0){
                _unitOfWork.Repository<ProductVariation>().Add(model);
            }else{
                ProductVariationSpecification spec = new ProductVariationSpecification(model.Id);
                var productVariation = await _unitOfWork.Repository<ProductVariation>().GetEntityWithSpec(spec);
                productVariation.Variation = model.Variation ?? productVariation.Variation;
                productVariation.IMEI = model.IMEI ?? productVariation.IMEI;
                productVariation.Quantity = model.Quantity;
                productVariation.SellingPrice = model.SellingPrice ;
                productVariation.SKU = model.SKU ?? productVariation.SKU;
                productVariation.DiscountPrice = model.DiscountPrice;
                _unitOfWork.Repository<ProductVariation>().Update(productVariation);
            }
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<int> AddOrUpdateProductImage(ProductImage model){
            ProductImageSpecification spec = new ProductImageSpecification(model.ProductId,true);
             var productImage = await _unitOfWork.Repository<ProductImage>().GetEntityWithSpec(spec);
            if(productImage == null){
                _unitOfWork.Repository<ProductImage>().Add(model);
            }else{
                productImage.MainImage = model.MainImage ?? productImage.MainImage;
                productImage.ImageOne = model.ImageOne ?? productImage.ImageOne;
                productImage.ImageTwo = model.ImageTwo ?? productImage.ImageTwo;
                productImage.ImageThree = model.ImageThree ?? productImage.ImageThree;
                productImage.ImageFour = model.ImageFour ?? productImage.ImageFour ;
                productImage.ImageFive = model.ImageFive ?? productImage.ImageFive;
                productImage.ImageSix = model.ImageSix ?? productImage.ImageSix;
                _unitOfWork.Repository<ProductImage>().Update(productImage);
            }
            int created = await _unitOfWork.Complete();
            return created;
        }


        public async Task<ProductSpecification> GetProductSpecification(String productIdString){
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(productIdString.ToLower().Trim());
             var products = await _unitOfWork.Repository<Product>().GetEntityWithSpec(pSpec);
            if(products == null) return  new ProductSpecification();
            ProductSpecificationsSpecification spec = new ProductSpecificationsSpecification(products.Id,true);
            var productSpecs = await _unitOfWork.Repository<ProductSpecification>().GetEntityWithSpec(spec);
            return productSpecs;
        }

        public async Task<string> UpdateProduct(Product product){
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(product.ProductIdString.ToLower().Trim());
            var products = await _unitOfWork.Repository<Product>().GetEntityWithSpec(pSpec);
            if(product == null) return "99";
            products.AdditionalNote = product.AdditionalNote ?? products.AdditionalNote;
            products.BasePrice = product.BasePrice;
            products.BoxContent = product.BoxContent;
            products.BrandId = product.BrandId == 0? products.BrandId : product.BrandId;
            products.CategoryId = product.CategoryId == 0 ? products.CategoryId : product.CategoryId;
            products.ProductName = product.ProductName ?? products.ProductName;
            products.VendorId = product.VendorId == 0 ? products.VendorId : product.VendorId;
            products.ProductDescription = product.ProductDescription ?? products.ProductDescription;
            products.KeyFeatures = product.KeyFeatures ?? products.KeyFeatures;
            products.YoutubeLink = product.YoutubeLink ?? products.YoutubeLink;
            products.IsActive = product.IsActive;

             _unitOfWork.Repository<Product>().Update(products);
            int outcome = await _unitOfWork.Complete();
            if(outcome > 0) return "00";
            return "ER";
        }
        public async Task<List<Product>> GetProductByBrand(PaginationSpecification spec)
        {
            int brandId;
            if(!int.TryParse(spec.SearchBy,out brandId)) throw new Exception();
            BrandSpecification brandSpecification = new BrandSpecification(brandId);
            var brand = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(brandSpecification);
            if(brand == null) throw new Exception();
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(spec,brand);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products; 
        }

        public async Task<List<Product>> GetProductByCategory(string category,PaginationSpecification spec)
        {
            //int brandId;
            // if(!int.TryParse(spec.SearchBy,out brandId)) throw new Exception();
            // BrandSpecification brandSpecification = new BrandSpecification(brandId);
            // var brand = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(brandSpecification);
            // if(brand == null) throw new Exception();
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(category,spec);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products; 
        }

        public async Task<int> GetProductCount(string category,PaginationSpecification spec)
        {
            //int brandId;
            // if(!int.TryParse(spec.SearchBy,out brandId)) throw new Exception();
            // BrandSpecification brandSpecification = new BrandSpecification(brandId);
            // var brand = await _unitOfWork.Repository<Brand>().GetEntityWithSpec(brandSpecification);
            // if(brand == null) throw new Exception();
            ProductWithFiltersForCountSpecificication pSpec = new ProductWithFiltersForCountSpecificication(category,spec);
            var products = await _unitOfWork.Repository<Product>().CountAsync(pSpec);
            return products; 
        }

        public async Task<List<Product>> GetProductByCategory(PaginationSpecification spec)
        {
            int categoryId;
            if(!int.TryParse(spec.SearchBy,out categoryId)) throw new Exception();
            CategorySpecification categorySpecification = new CategorySpecification(categoryId);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpecification);
            if(category == null) throw new Exception();
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(spec,category);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products; 
        }

        public async Task<List<Product>> GetProductByCategory(long categoryId)
        {
            List<long> catArray = new List<long>();
            CategorySpecification categorySpecification = new CategorySpecification(categoryId);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpecification);
            if(category == null) throw new Exception();
            catArray.Add(category.Id);
            if(category.ParentId != 0) catArray.Add(category.ParentId);

            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(catArray.ToArray());
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products; 
        }

        public async Task<Product> GetProductByProductId(string productId){
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(productId.ToLower().Trim());
             var products = await _unitOfWork.Repository<Product>().GetEntityWithSpec(pSpec);
             return products;
        }
        public async Task<List<Product>> GetProductByVendor(PaginationSpecification spec)
        {
            int vendorId;
            if(!int.TryParse(spec.SearchBy,out vendorId)) throw new Exception();
            VendorSpecification vendorSpecification = new VendorSpecification(vendorId);
            var vendor = await _unitOfWork.Repository<Vendor>().GetEntityWithSpec(vendorSpecification);
            if(vendor == null) throw new Exception();
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(spec,vendor);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products;
        }

        public async Task<List<Product>> GetProducts(PaginationSpecification spec)
        {
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(spec);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products; 
        }

        public async Task<List<Product>> GetDealOfWeekProducts(int take)
        {
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(take,true);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products.Take(take).ToList(); 
        }

        public async Task<List<Product>> GetFeaturedProducts(int take)
        {
            productSpec.ProductSpecification pSpec = new productSpec.ProductSpecification(true);
            var products = await _unitOfWork.Repository<Product>().ListAsync(pSpec);
            return (List<Product>)products.Take(take).ToList(); 
        }

        public async Task<IReadOnlyList<Product>> getProductDataTable(DataTableRequestSpecification spec)
        {
            productSpec.ProductSpecification specification = new productSpec.ProductSpecification(spec);
            var products = await _unitOfWork.Repository<Product>().ListAsync(specification);
            return products;
        }

         public  async Task<int> getCount()
        {
            productSpec.ProductSpecification specification = new productSpec.ProductSpecification();
            var product = await _unitOfWork.Repository<Product>().CountAsync(specification);
            return product;
        }
    }
}