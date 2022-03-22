using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> SelectAllCategories();
        Task<Category> SelectCategoryById(int categoryId);
        Task<Category> InsertNewCategory(Category category);
        Task<Category> DeleteCategoryById(int categoryId);
        Task<Category> UpdateCategoryById(int categoryId, Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly MotorDBContext _dBContext;

        public CategoryRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Category> DeleteCategoryById(int categoryId)
        {
            Category categoryToDelete = await _dBContext
                .Category
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (categoryToDelete != null)
            {
                _dBContext.Remove(categoryToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return categoryToDelete;
        }

        public async Task<Category> InsertNewCategory(Category category)
        {
            await _dBContext.AddAsync(category);
            await _dBContext.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> SelectAllCategories()
        {
            return await _dBContext
                .Category
                .ToListAsync();
        }

        public async Task<Category> SelectCategoryById(int categoryId)
        {
            return await _dBContext
                .Category
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<Category> UpdateCategoryById(int categoryId, Category category)
        {
            Category categoryToUpdate = await _dBContext
                .Category
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.CategoryName = category.CategoryName;

                await _dBContext.SaveChangesAsync();
            }

            return categoryToUpdate;
        }
    }
}
