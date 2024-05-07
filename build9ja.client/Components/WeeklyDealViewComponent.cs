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
    public class WeeklyDealViewComponent : ViewComponent
    {
        private readonly ILogger<WeeklyDealViewComponent> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public WeeklyDealViewComponent(ILogger<WeeklyDealViewComponent> logger,IProductService productService,IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProductDto> productDto = new List<ProductDto>();
            try
            {
                var product = await _productService.GetDealOfWeekProducts(8);
                productDto = _mapper.Map<List<Product>,List<ProductDto>>(product);
            }
            catch (System.Exception e)
            {
                _logger.LogError("WeeklyDealViewComponent",e);
            }
            return View(productDto);
        }
    }
}