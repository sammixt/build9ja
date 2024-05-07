using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using build9ja.core.Entities;
using build9ja.core.Specifications;

namespace build9ja.core.Interfaces
{
    public interface ICategoryService
    {
        //add catgory
        Task<int> CreateCategory(Category category);
        //update category
        Task<string> UpdateCategory(long id, Category category);
        //update status
        Task<string> UpdateCategoryStatus(long id, string status);
        //get category by id
        Task<Category> GetCategoryById(long id);
        //get category by parentId
        Task<List<Category>> GetCategoryByParentId(long parentId, bool idType);
        Task<List<Category>> GetCategories();
        //get categoryy by name
        Task<Category> GetCategoryByName(string name);
        Task<List<Category>> getCategoryDataTable(DataTableRequestSpecification spec);
        Task<int> getCount();
        Task<List<Category>> GetTopCategories();
    }
}