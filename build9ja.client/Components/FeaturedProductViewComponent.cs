using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto.Products;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.client.Components
{
    public class FeaturedProductViewComponent  : ViewComponent
    {

        private readonly ILogger<FeaturedProductViewComponent> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public FeaturedProductViewComponent(ILogger<FeaturedProductViewComponent> logger,IProductService productService,
        IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FeaturedProductDto featuredProductDto = 
            new FeaturedProductDto();
            List<ProductDto> productDto = new List<ProductDto>();
            try
            {
                var product = await _productService.GetFeaturedProducts(30);
                productDto = _mapper.Map<List<Product>,List<ProductDto>>(product);
                featuredProductDto.FeatureA = productDto.Skip(0).Take(3).ToList();
                featuredProductDto.FeatureB = productDto.Skip(3).Take(3).ToList();
                featuredProductDto.FeatureC = productDto.Skip(6).Take(3).ToList();
                featuredProductDto.FeatureD = productDto.Skip(9).Take(3).ToList();
            }
            catch (System.Exception e)
            {
                _logger.LogError("WeeklyDealViewComponent",e);
            }
            return View(featuredProductDto);
        }
    }
}