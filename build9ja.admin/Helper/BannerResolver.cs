using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.core.Entities;

namespace build9ja.admin.Helper
{
    public class BannerResolver : IMemberValueResolver<BannerDto,Banner, string, string>
    {
        private readonly IConfiguration _config;
        public BannerResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(BannerDto source, Banner destination, string sourceMember, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
               return $"{_config["AppUrl"]}//Uploads//Banner//{sourceMember}";
            }
            return null;
        }
    }
}