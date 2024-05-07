using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.core.Entities;

namespace build9ja.admin.Helper
{
    public class BrandUrlRsolver : IValueResolver<BrandDto,Brand, string>
    {
        private readonly IConfiguration _config;
        public BrandUrlRsolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(BrandDto source,Brand  destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.BrandLogo))
            {
                return $"{_config["AppUrl"]}//Uploads//Brand//{source.BrandLogo}";
            }
            return null;
        }
    }
}