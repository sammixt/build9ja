using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.api.Dto;
using build9ja.api.DtoUtility;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.api.Controllers
{
    
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto model)
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
            var category = _mapper.Map<CategoryDto, Category>(model);
            var create = await  _categoryService.CreateCategory(category);
            if(create > 0) return Ok(model);
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "An error has occured"));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] CategoryDto model)
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
            var category = _mapper.Map<CategoryDto, Category>(model);
            if (await _categoryService.UpdateCategory(id,category) == "00") return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "unable to update category at this time"));
        }
        
        //update vendor status
        [HttpPut("update/status/{id}")]
        public async Task<ActionResult> UpdateStatus(long id, [FromBody] StatusDto model)
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
            if (await _categoryService.UpdateCategoryStatus(id, model.Status) == "00") return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError,new ApiResponse(500, "Unable to update  status at this time"));
        }
        [HttpGet]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> Get()
        {
            var categories = await _categoryService.GetCategoryByParentId(0,false);
            if(!categories.Any()) return Ok(new List<CategoryDto>());
            var categoriesDto = _mapper.Map<List<Category>, List<CategoryDto>>(categories);
            foreach(var categoryDto in categoriesDto){
                //if(categoryDto.ParentId != 0){
                    var categoriesByPId = await _categoryService.GetCategoryByParentId(categoryDto.Id,false);
                     var cateListDto =  _mapper.Map<List<Category>, List<CategoryListDto>>(categoriesByPId);
                     categoryDto.SubCategories = cateListDto;
               // }
            }
            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> Get(long id)
        {
            var category = await _categoryService.GetCategoryById(id);
            var categoriesDto = _mapper.Map<Category, CategoryDto>(category);
            if(categoriesDto != null && categoriesDto.ParentId != 0){
               var cat =   await _categoryService.GetCategoryById(categoriesDto.ParentId);
               categoriesDto.ParentCategory = cat.Name;
            }
            return Ok(categoriesDto);
        }

        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<ActionResult> GetByName(string name)
        {
            var category = await _categoryService.GetCategoryByName(name);
            var categoriesDto = _mapper.Map<Category, CategoryDto>(category);
            if(categoriesDto != null && categoriesDto.ParentId != 0){
               var cat =   await _categoryService.GetCategoryById(categoriesDto.ParentId);
               categoriesDto.ParentCategory = cat.Name;
            }
            return Ok(categoriesDto);
        }
    }
}