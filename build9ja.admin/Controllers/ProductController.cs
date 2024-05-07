using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.admin.Dto;
using build9ja.admin.Dto.Products;
using build9ja.admin.enums;
using build9ja.admin.Extensions;
using build9ja.admin.Helper;
using build9ja.admin.Responses;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductSpecification = build9ja.core.Entities.ProductSpecification;

namespace build9ja.admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
         private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(ILogger<ProductController> logger, IMapper mapper,
        IProductService productService, IUnitOfWork unitOfWork, ICategoryService categoryService,
        IBrandService brandService,IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _brandService = brandService;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Detail(string pid, string stp)
        {
           if(string.IsNullOrEmpty(pid)) return RedirectToAction(nameof(Index));
            // var categories = await _categoryService.GetCategories();
            // var categoriesDto = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            // var brands = await _brandService.GetBrands();
            // var brandsDto = _mapper.Map<List<Brand>,List<BrandDto>>(brands);
            // ViewBag.Brand = brandsDto;
            // ViewBag.Category = categoriesDto;
            var product = await _productService.GetProductByProductId(pid);
             var productDto = _mapper.Map<Product,ProductDto>(product);
            return View(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]ProductDto model)
        {
            
            try{
                if (!ModelState.IsValid)
                    {
                        var modelErrors = new List<string>();
                        foreach (var modelState in ModelState.Values)
                        {
                            foreach (var modelError in modelState.Errors)
                            {
                                modelErrors.Add(modelError.ErrorMessage);
                            }
                        }
                        return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                    }
                    string guid = Guid.NewGuid().ToString("N");
                    
                    var productId = guid+ DateTime.Now.ToString("ddMMyyyyHHmmss") ;
                    var modelEntity = new Product(){
                        ProductName = model.ProductName,
                        CategoryId = model.CategoryId,
                        VendorId = model.VendorId,
                        BasePrice = model.BasePrice,
                        ProductIdString = productId,
                        Status = ProductStatus.Pending.ToString()
                    };
                    _unitOfWork.Repository<Product>().Add(modelEntity);
                    await _unitOfWork.Complete();
                    return Ok(new ApiResponse(200,"Product created"));
            }
            catch (System.Exception e)
            {
                 _logger.LogError("AddProduct",e);
            }

            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add product at this time"));

        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                
                var product = _mapper.Map<ProductDto, Product>(model);
                if (await _productService.UpdateProduct(product) == "00") return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateProduct",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update product at this time"));
        }

        [HttpPatch]
        public async Task<ActionResult> PatchProductSpecification([FromBody] ProductSpecificationDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                var product = await _productService.GetProductByProductId(model.ProductIdString);
                if(product == null) return BadRequest(new ApiResponse(400,"Product not found"));
                var productSpec = _mapper.Map<ProductSpecificationDto, ProductSpecification>(model);
                productSpec.ProductId = product.Id;
                productSpec.Product = product;
                if (await _productService.AddOrUpdateProductSpecification(productSpec) > 0) return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("PatchProductSpecification",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update product specification at this time"));
        }

        [HttpPatch]
        public async Task<ActionResult> PatchProductVariation([FromBody] ProductVariationDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                var product = await _productService.GetProductByProductId(model.ProductIdString);
                if(product == null) return BadRequest(new ApiResponse(400,"Product not found"));
                var productVariation = _mapper.Map<ProductVariationDto, ProductVariation>(model);
                productVariation.ProductId = product.Id;
                productVariation.Product = product;
                if (await _productService.AddOrUpdateProductVariation(productVariation) > 0) return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("PatchProductVariation",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update product variation at this time"));
        }

        [HttpPatch]
         public async Task<ActionResult> PatchProductImage([FromForm] ProductImageDto model)
        {
            Func<string[], string ,bool> getImageType = (a,b) => {
                if(a.Contains(b)){
                   return true;
                }
                return false;
            };
            try
            {
                string fileName  = null;
                if (!ModelState.IsValid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                 var product = await _productService.GetProductByProductId(model.ProductIdString);
                if(product == null) return BadRequest(new ApiResponse(400,"Product not found"));

               
                if(model.UploadImage == null) return BadRequest(new ApiResponse(400,"File is required"));
                
                fileName = Utility.ProcessUploadedFile(model.UploadImage,GetPathAndFilename());

               var imageTypes = model.ImageType.Split(',');
               model.MainImage = getImageType(imageTypes,"MainImage") ? fileName : null;
               model.ImageOne = getImageType(imageTypes,"ImageOne") ? fileName : null;
               model.ImageTwo = getImageType(imageTypes,"ImageTwo") ? fileName : null;
               model.ImageThree = getImageType(imageTypes,"ImageThree") ? fileName : null;
               model.ImageFour = getImageType(imageTypes,"ImageFour") ? fileName : null;
               model.ImageFive = getImageType(imageTypes,"ImageFive") ? fileName : null;
               model.ImageSix = getImageType(imageTypes,"ImageSix") ? fileName : null;
                var productImage = _mapper.Map<ProductImageDto, ProductImage>(model);
                productImage.Product = product;
                productImage.ProductId = product.Id;
                if (await _productService.AddOrUpdateProductImage(productImage) > 0) return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("PatchProductImage",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update image at this time"));
        }

        [HttpPost]
        public async Task<ActionResult> GetProducts()
        {
            var DataTableRequest = Request.GetDataTableRequestForm();
            var drRequest = _mapper.Map<DataTableRequestDto,DataTableRequestSpecification>(DataTableRequest);
            int count = await _productService.getCount();
            var resultSet = await _productService.getProductDataTable(drRequest);
            var results = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(resultSet);
            var output = new DataTableDto<ProductDto> {  
                    Draw = Convert.ToInt32(DataTableRequest.Draw),  
                    Data = results,  
                    RecordsFiltered = count,  
                    RecordsTotal = count  
                };
            return Ok(output);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStatus([FromBody] ProductStatusDto model){
            try
            {
                int update = await _productService.UpdateProductStatus(model.ProductIdString, model.Status, model.IsFeatured);
                if(update == 404) return BadRequest(new ApiResponse(400,"Product not found"));
                if(update > 0) return Ok(new ApiResponse(200,"Product Status Update"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateProduct",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update product at this time"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Product/";
        }
    }
}