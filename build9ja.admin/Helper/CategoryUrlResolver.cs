using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.core.Entities;

namespace build9ja.admin.Helper
{
    public class CategoryUrlResolver : IValueResolver<CategoryDto,Category, string>
    {
        private IConfiguration _config;
        public CategoryUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(CategoryDto source,Category  destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Image))
            {
                return $"{_config["AppUrl"]}/Uploads/Category/{source.Image}";
            }
            return null;
        }
    }
}