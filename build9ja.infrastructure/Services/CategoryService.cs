using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Interfaces;
using build9ja.core.Specifications;
using build9ja.infrastructure.Data;

namespace build9ja.infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateCategory(Category category)
        {
            _unitOfWork.Repository<Category>().Add(category);
            int created = await _unitOfWork.Complete();
            return created;
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            return (List<Category>)categories;
        }

        public async Task<Category> GetCategoryById(long id)
        {
            CategorySpecification spec = new CategorySpecification(id);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(spec);
            return category;
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            if(string.IsNullOrEmpty(name)) return null;
             CategorySpecification spec = new CategorySpecification(name.ToLower());
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(spec);
            return category;
        }

        public async Task<List<Category>> GetCategoryByParentId(long parentId,  bool idType)
        {
            CategorySpecification spec = new CategorySpecification(parentId,idType);
            var categories = await _unitOfWork.Repository<Category>().ListAsync(spec);
            return (List<Category>)categories;
        }

        public async Task<string> UpdateCategory(long id, Category category)
        {
            CategorySpecification spec = new CategorySpecification(id);
            var model = await _unitOfWork.Repository<Category>().GetEntityWithSpec(spec);
            if(model == null) return "NE";
            model.Name =category.Name??model.Name;
            model.Description = category.Description??model.Description;
            model.Image = category.Image??model.Image;
            model.ParentId = category.ParentId;
            _unitOfWork.Repository<Category>().Update(model);
            int outcome = await _unitOfWork.Complete();
            if(outcome > 0) return "00";
            return "ER";

        }

        public async Task<string> UpdateCategoryStatus(long id, string status)
        {
            if(string.IsNullOrEmpty(status)) return "NE";
            CategorySpecification spec = new CategorySpecification(id);
            var model = await _unitOfWork.Repository<Category>().GetEntityWithSpec(spec);
            if(model == null) return "NE";
            model.Status = status;
             _unitOfWork.Repository<Category>().Update(model);
            int outcome = await _unitOfWork.Complete();
            if(outcome > 0) return "00";
            return "ER";
        }
    }
}