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
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryController(ILogger<CategoryController> logger,ICategoryService categoryService, IMapper mapper,
        IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetCategories()
        {
            Func<long,Task<string>> getParent = async (a) =>  {
                if(a!= 0){
                var cat =   await _categoryService.GetCategoryById(a);
                return  cat.Name;
                }
                return "";
            };

            var DataTableRequest = Request.GetDataTableRequestForm();
            var drRequest = _mapper.Map<DataTableRequestDto,DataTableRequestSpecification>(DataTableRequest);
            int count = await _categoryService.getCount();
            var resultSet = await _categoryService.getCategoryDataTable(drRequest);

            var results = _mapper.Map<List<Category>,List<CategoryDto>>(resultSet);
            foreach(var item in results){
                item.ParentCategory = await getParent(item.ParentId);
            }
            
            var output = new DataTableDto<CategoryDto> {  
                    Draw = Convert.ToInt32(DataTableRequest.Draw),  
                    Data = results,  
                    RecordsFiltered = count,  
                    RecordsTotal = count  
                };
            return Ok(output);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetCategories();
            if(!categories.Any()) return Ok(new List<CategoryDto>());
            var categoriesDto = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            
            return Ok(categoriesDto);
        }

        
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> GetById(long id)
        {
            var category = await _categoryService.GetCategoryById(id);
            var categoriesDto = _mapper.Map<Category, CategoryDto>(category);
            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromForm]  CategoryDto model)
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
                model.Image = Utility.ProcessUploadedFile(model.UploadImage,GetPathAndFilename());
                var category = _mapper.Map<CategoryDto, Category>(model);
                var create = await  _categoryService.CreateCategory(category);
                if(create > 0) return Ok(new ApiResponse(200,"Successful"));
                return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "An error has occured"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("AddCategory",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to add category at this time"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory([FromForm] CategoryDto model)
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
                    model.Image = Utility.ProcessUploadedFile(model.UploadImage,GetPathAndFilename());
                }
                var category = _mapper.Map<CategoryDto, Category>(model);
                if (await _categoryService.UpdateCategory(model.Id,category) == "00") return Ok(new ApiResponse(200,"Successful"));
            }
             catch (System.Exception e)
            {
                 _logger.LogError("UpdateCategory",e);
            }
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update category at this time"));
        }

        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Category/";
        }
        
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}