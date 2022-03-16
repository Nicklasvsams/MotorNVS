using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MotorNVS.Test.MotorNVS.DAL.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly MotorDBContext _dBContext;
        private readonly DbContextOptions<MotorDBContext> _options;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<MotorDBContext>()
                .UseInMemoryDatabase(databaseName: "MotorCategory")
                .Options;
            _dBContext = new MotorDBContext(_options);
            _categoryRepository = new CategoryRepository(_dBContext);
        }

        [Fact]
        public async void GetAllCategories_ReturnsListOfCategories_WhenCategoriesExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.AddRange(CategoryList());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.SelectAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllCategories_ReturnsEmptyList_WhenCategoriesDoNotExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.SelectAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetCategoryById_ReturnsSingleCategory_WhenCategoryIdExists()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.Add(Category());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Test", result.CategoryName);
        }

        [Fact]
        public async void GetCategoryById_ReturnsNull_WhenCategoryIdDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteCategoryById_ReturnsCategory_WhenCategoryToDeleteExists()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.Add(Category());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.DeleteCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        public async void DeleteCategoryById_ReturnsNull_WhenCategoryIdDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.DeleteCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewCategory_ShouldAddIdAndReturnCategory_WhenCategoryIsSuccessfullyInserted()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            Category category = new Category()
            {
                CategoryName = "Test"
            };

            // Act
            var result = await _categoryRepository.InsertNewCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.CategoryName);
        }

        [Fact]
        public async void InsertNewCategory_ShouldFailToAddCategory_WhenCategoryWithSameIdAlreadyExists()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            Category category = new Category()
            {
                Id = 1,
                CategoryName = "Test"
            };

            _dBContext.Category.Add(category);

            await _dBContext.SaveChangesAsync();

            // Act
            async Task action() => await _categoryRepository.InsertNewCategory(category);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateCategoryById_ShouldReturnCategory_WhenCategoryIsSuccessfullyUpdated()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            Category category = new Category()
            {
                CategoryName = "Test"
            };

            _dBContext.Category.Add(category);

            await _dBContext.SaveChangesAsync();

            Category categoryUpdate = new Category()
            {
                CategoryName = "Test2"
            };

            // Act
            var result = await _categoryRepository.UpdateCategoryById(categoryId, categoryUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test2", result.CategoryName);
        }

        [Fact]
        public async void UpdateCategoryById_ShouldReturnNull_WhenCategoryToUpdateDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            Category categoryUpdate = new Category()
            {
                CategoryName = "Test2"
            };

            // Act
            var result = await _categoryRepository.UpdateCategoryById(categoryId, categoryUpdate);

            // Assert
            Assert.Null(result);
        }

        private List<Category> CategoryList()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    CategoryName = "Test"
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "Test2"
                }
            };
        }

        private Category Category()
        {
            return new Category()
            {
                Id = 1,
                CategoryName = "Test"
            };
        }
    }
}
