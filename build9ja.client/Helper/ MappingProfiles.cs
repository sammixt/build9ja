using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto;
using build9ja.client.Dto.Products;
using build9ja.core.Entities;
using build9ja.core.Entities.Identity;

namespace build9ja.client.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryAndSubDto>();
			CreateMap<Category,CategoryListDto>().ReverseMap();
            CreateMap<Banner, BannerDto>();
			
			CreateMap<core.Entities.ProductSpecification, ProductSpecificationDto>().ReverseMap();
			CreateMap<ProductVariation,ProductVariationDto>().ReverseMap();
			CreateMap<ProductImage, ProductImageDto>();
			CreateMap<Product,ProductDto>()
				 .ForMember(x => x.Category, o => o.MapFrom(s => s.Category.Name))
				 .ForMember(x => x.Brand, o => o.MapFrom(s => s.Brand.BrandName))
				 .ForMember(x => x.Vendor, o => o.MapFrom(s => s.Vendor.Company));

			CreateMap<ProductDto, Product>();
			CreateMap<Product, ProductExtDto>()
			.ForMember(x => x.Category, o => o.MapFrom(s => s.Category.Name))
				 .ForMember(x => x.Brand, o => o.MapFrom(s => s.Brand.BrandName))
				 .ForMember(x => x.Vendor, o => o.MapFrom(s => s.Vendor.Company));
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
			CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
			CreateMap<WishList, WishListDto>().ReverseMap();
			CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}