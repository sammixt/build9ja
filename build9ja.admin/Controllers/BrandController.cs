using AutoMapper;
using build9ja.admin.Dto;
using build9ja.admin.Extensions;
using build9ja.admin.Helper;
using build9ja.admin.Responses;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.admin.Controllers
{
    public class BrandController : Controller
    {
        private readonly ILogger<BrandController> _logger;
         private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger,IBrandService brandService,IMapper mapper,
        IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _brandService = brandService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetBrands()
        {
           
            var DataTableRequest = Request.GetDataTableRequestForm();
            var drRequest = _mapper.Map<DataTableRequestDto,DataTableRequestSpecification>(DataTableRequest);
            int count = await _brandService.getCount();
            var resultSet = await _brandService.getBrandDataTable(drRequest);

            var results = _mapper.Map<List<Brand>,List<BrandDto>>(resultSet);
            
            
            var output = new DataTableDto<BrandDto> {  
                    Draw = Convert.ToInt32(DataTableRequest.Draw),  
                    Data = results,  
                    RecordsFiltered = count,  
                    RecordsTotal = count  
                };
            return Ok(output);
        }

        [ProducesResponseType(typeof(BrandDto), 200)]
        public async Task<ActionResult> GetById(long id)
        {
            var brand = await _brandService.GetBrandById(id);
            var brandDto = _mapper.Map<Brand, BrandDto>(brand);
            return Ok(brandDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddBrand([FromForm]  BrandDto model)
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

                if(model.UploadImage == null) return BadRequest(new ApiResponse(400,"Image is required"));
                model.BrandLogo = Utility.ProcessUploadedFile(model.UploadImage,GetPathAndFilename());
                var brand = _mapper.Map<BrandDto, Brand>(model);
                var create = await  _brandService.CreateBrand(brand);
                if(create > 0) return Ok(new ApiResponse(200,"Successful"));
                return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "An error has occured"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("AddBrand",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add brand at this time"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBrand([FromForm] BrandDto model)
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
                if(model.UploadImage != null) 
                {
                    model.BrandLogo = Utility.ProcessUploadedFile(model.UploadImage,GetPathAndFilename());
                }
                var brand = _mapper.Map<BrandDto, Brand>(model);
                if (await _brandService.UpdateBrand(brand) == "00") return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateBrand",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update brand at this time"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> GetAllBrand()
        {
            var brands = await _brandService.GetBrands();
            if(!brands.Any()) return Ok(new List<BrandDto>());
            var brandsDto = _mapper.Map<List<Brand>, List<BrandDto>>(brands);
            
            return Ok(brandsDto);
        }

        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Brand/";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}