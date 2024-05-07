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
    public class RelatedProductViewComponent : ViewComponent
    {
        private readonly ILogger<RelatedProductViewComponent> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public RelatedProductViewComponent(ILogger<RelatedProductViewComponent> logger,IProductService productService,
        IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(long categoryId)
        {
            
            List<ProductDto> productDto = new List<ProductDto>();
            try
            {
                var product = await _productService.GetProductByCategory(categoryId);
                productDto = _mapper.Map<List<Product>,List<ProductDto>>(product);
            }
            catch (System.Exception e)
            {
                _logger.LogError("RelatedProductViewComponent",e);
            }
            return View(productDto);
        }
    }
}