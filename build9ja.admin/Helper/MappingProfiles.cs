using System;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.admin.Dto.Products;
using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.admin.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Staff, StaffDto>()
				.ForMember(d => d.Address, o => o.MapFrom(s => s.Contact.Address))
				.ForMember(d => d.AltPhoneNumber, o => o.MapFrom(s => s.Contact.AltPhoneNumber))
				.ForMember(d => d.City, o => o.MapFrom(s => s.Contact.City))
				.ForMember(d => d.Country, o => o.MapFrom(s => s.Contact.Country))
				.ForMember(d => d.State, o => o.MapFrom(s => s.Contact.State))
				.ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.Contact.PhoneNumber))
				.ForMember(d => d.EmailAddress, o => o.MapFrom(s => s.Contact.EmailAddress))
				.ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
				.ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
				.ForMember(d => d.Sex, o => o.MapFrom(s => s.Sex))
				.ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth))
				.ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
				.ForMember(d => d.StaffId, o => o.MapFrom(s => s.StaffId))
				.ForMember(d => d.ImageLocation, o => o.MapFrom(s => s.ImageLocation))
				.ReverseMap();
			CreateMap<DataTableRequestDto,DataTableRequestSpecification>().ReverseMap();
			CreateMap<Permission, PermissionDto>().ReverseMap();
			CreateMap<Commission,CommissionDto>().ReverseMap();
			CreateMap<Vendor,VendorDto>().ReverseMap();
			CreateMap<VendorBankInfo,VendorBankInfoDto>().ReverseMap();
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto,Category>()
                .ForMember(b => b.Image, o => o.MapFrom<CategoryUrlResolver>());
			CreateMap<Category,CategoryListDto>().ReverseMap();
			CreateMap<Brand, BrandDto>();
                
            CreateMap<BrandDto, Brand>()
			.ForMember(b => b.BrandLogo, o => o.MapFrom<BrandUrlRsolver>());

			CreateMap<ProductImageDto,ProductImage>()
				.ForMember(b => b.MainImage, o => o.MapFrom<ProductImageResolver,string>(src => src.MainImage))
                .ForMember(b => b.ImageOne, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageOne))
                .ForMember(b => b.ImageTwo, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageTwo))
                .ForMember(b => b.ImageThree, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageThree))
                .ForMember(b => b.ImageFour, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageFour))
                .ForMember(b => b.ImageFive, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageFive))
                .ForMember(b => b.ImageSix, o => o.MapFrom<ProductImageResolver, string>(src => src.ImageSix));
			
			CreateMap<core.Entities.ProductSpecification, ProductSpecificationDto>().ReverseMap();
			CreateMap<ProductVariation,ProductVariationDto>().ReverseMap();
			CreateMap<ProductImage, ProductImageDto>();
			CreateMap<Product,ProductDto>()
				 .ForMember(x => x.Category, o => o.MapFrom(s => s.Category.Name))
				 .ForMember(x => x.Brand, o => o.MapFrom(s => s.Brand.BrandName))
				 .ForMember(x => x.Vendor, o => o.MapFrom(s => s.Vendor.Company));

			CreateMap<ProductDto, Product>();
			CreateMap<Banner, BannerDto>();
			CreateMap<BannerDto, Banner>()
			.ForMember(x => x.ImageOne, o => o.MapFrom<BannerResolver, string>(src => src.ImageOne))
			.ForMember(x => x.ImageTwo, o => o.MapFrom<BannerResolver, string>(src => src.ImageTwo))
			.ForMember(x => x.ImageThree, o => o.MapFrom<BannerResolver, string>(src => src.ImageThree))
			.ForMember(x => x.ImageFour, o => o.MapFrom<BannerResolver, string>(src => src.ImageFour))
			.ForMember(x => x.SubPageImage, o => o.MapFrom<BannerResolver, string>(src => src.SubPageImage));

			CreateMap<DeliveryMethod,DeliveryMethodDto>().ReverseMap();
			}

			
	}
}

