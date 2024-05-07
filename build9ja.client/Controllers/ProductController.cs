using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto.Products;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.client.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

         public ProductController(ILogger<ProductController> logger, IMapper mapper,
        IProductService productService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> Detail([FromQuery]string pid)
        {
            var product = await _productService.GetProductByProductId(pid);
             var productDto = _mapper.Map<Product,ProductDto>(product);
            return Ok(productDto);
        }

       
    }
}