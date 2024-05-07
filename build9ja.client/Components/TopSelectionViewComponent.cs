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
    public class TopSelectionViewComponent : ViewComponent
    {

        private readonly ILogger<TopSelectionViewComponent> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        
        public TopSelectionViewComponent(ILogger<TopSelectionViewComponent> logger,IProductService productService,IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TopProductSelectionDto topProductSelectionDto = 
            new TopProductSelectionDto();
            List<ProductDto> productDto = new List<ProductDto>();
            try
            {
                var product = await _productService.GetDealOfWeekProducts(30);
                productDto = _mapper.Map<List<Product>,List<ProductDto>>(product);
                topProductSelectionDto.NewArrival = productDto.OrderBy(d => d.DateCreated).Take(8).ToList();
                 topProductSelectionDto.TopSelection = productDto.Take(8).ToList();
            }
            catch (System.Exception e)
            {
                _logger.LogError("TopSelectionViewComponent",e);
            }
            return View(topProductSelectionDto);
        }
    }
}