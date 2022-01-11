using System;
using AutoMapper;
using build9ja.api.Dto;
using build9ja.core.Entities;

namespace build9ja.api.Helper
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
			CreateMap<Permission, PermissionDto>().ReverseMap();
			CreateMap<Commission,CommissionDto>().ReverseMap();
			CreateMap<Vendor,VendorDto>().ReverseMap();
			CreateMap<VendorBankInfo,VendorBankInfoDto>().ReverseMap();
		}
	}
}

