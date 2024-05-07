using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto.Products;
using build9ja.core.Entities;

namespace build9ja.admin.Helper
{
    public class ProductImageResolver : IMemberValueResolver<ProductImageDto,ProductImage, string, string>
    {
        private readonly IConfiguration _config;

        public ProductImageResolver(IConfiguration config)
        {
            _config = config;
        }

       

        public string Resolve(ProductImageDto source, ProductImage destination, string sourceMember, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return $"{_config["AppUrl"]}//Uploads//Product//{sourceMember}";
            }
            return null;
        }
    }
}