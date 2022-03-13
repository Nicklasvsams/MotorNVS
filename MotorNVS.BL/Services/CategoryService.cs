using MotorNVS.BL.DTOs.CategoryDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<CategoryResponse> DeleteCategoryById(int categoryId);
        Task<CategoryResponse> CreateCategory(CategoryRequest newCategory);
        Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest categoryUpdate);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest newCategory)
        {
            Category createdCategory = await _categoryRepository.InsertNewCategory(MapCategoryRequestToCategory(newCategory));

            if (createdCategory != null)
            {
                return MapCategoryToCategoryResponse(createdCategory);
            }

            return null;
        }

        public async Task<CategoryResponse> DeleteCategoryById(int categoryId)
        {
            Category deletedCategory = await _categoryRepository.DeleteCategoryById(categoryId);

            if (deletedCategory != null)
            {
                return MapCategoryToCategoryResponse(deletedCategory);
            }

            return null;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categoryList = await _categoryRepository.SelectAllCategories();

            return categoryList.Select(x => MapCategoryToCategoryResponse(x)).ToList();
        }

        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category category = await _categoryRepository.SelectCategoryById(categoryId);

            if (category != null)
            {
                return MapCategoryToCategoryResponse(category);
            }

            return null;
        }

        public async Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest categoryUpdate)
        {
            Category updatedCategory = await _categoryRepository.UpdateCategoryById(categoryId, MapCategoryRequestToCategory(categoryUpdate));

            if (updatedCategory != null)
            {
                return MapCategoryToCategoryResponse(updatedCategory);
            }

            return null;
        }

        private static CategoryResponse MapCategoryToCategoryResponse(Category category)
        {
            return new CategoryResponse()
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            };
        }

        private static Category MapCategoryRequestToCategory(CategoryRequest categoryReq)
        {
            return new Category()
            {
                CategoryName = categoryReq.CategoryName
            };
        }
    }
}
