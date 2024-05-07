using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using build9ja.client.Dto;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace build9ja.client.Components
{
    public class HeaderViewComponent  : ViewComponent
    {
         private readonly ILogger<HeaderViewComponent> _logger;
         private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public HeaderViewComponent(ILogger<HeaderViewComponent> logger,ICategoryService categoryService,
        IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderCategoryDto headerCategoryDto = new HeaderCategoryDto();
            try
            {
                var categories = await _categoryService.GetCategoryByParentId(0,false);
                var categoriesDto = _mapper.Map<List<Category>, List<CategoryAndSubDto>>(categories);
                foreach(var categoryDto in categoriesDto){
                    var categoriesByPId = await _categoryService.GetCategoryByParentId(categoryDto.Id,false);
                    var cateListDto =  _mapper.Map<List<Category>, List<CategoryListDto>>(categoriesByPId);
                    categoryDto.SubCategories = cateListDto;
                }

                headerCategoryDto.CategoryOne = categoriesDto.Skip(0).Take(5).ToList();
                headerCategoryDto.CategoryTwo = categoriesDto.Skip(5).Take(5).ToList();
                headerCategoryDto.CategoryThree = categoriesDto.Skip(10).Take(5).ToList();
                headerCategoryDto.CategoryFour = categoriesDto.Skip(15).Take(5).ToList();
            }
            catch (System.Exception e)
            {
                  _logger.LogError("HeaderViewComponent",e);
            }
             return View(headerCategoryDto);
        }
    }
}