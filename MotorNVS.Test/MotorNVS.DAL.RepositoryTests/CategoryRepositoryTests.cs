using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;
using System.Collections.Generic;
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
        public async void GetAllFuels_ReturnsListOfFuels_WhenFuelsExist()
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
        public async void GetAllFuels_ReturnsEmptyList_WhenFuelsDoNotExist()
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

        //[Fact]
        //public async void GetFuelById_ReturnsSingleFuel_WhenFuelIdExists()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    _dBContext.Add(Fuel());

        //    await _dBContext.SaveChangesAsync();

        //    // Act
        //    var result = await _fuelRepository.SelectFuelById(fuelId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(fuelId, result.Id);
        //}

        //[Fact]
        //public async void GetFuelById_ReturnsNull_WhenFuelIdDoesNotExist()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    // Act
        //    var result = await _fuelRepository.SelectFuelById(fuelId);

        //    // Assert
        //    Assert.Null(result);
        //}

        //[Fact]
        //public async void DeleteFuelById_ReturnsFuel_WhenFuelToDeleteExists()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    _dBContext.Add(Fuel());

        //    await _dBContext.SaveChangesAsync();

        //    // Act
        //    var result = await _fuelRepository.DeleteFuelById(fuelId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(fuelId, result.Id);
        //}

        //[Fact]
        //public async void DeleteFuelById_ReturnsNull_WhenFuelIdDoesNotExist()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    // Act
        //    var result = await _fuelRepository.DeleteFuelById(fuelId);

        //    // Assert
        //    Assert.Null(result);
        //}

        //[Fact]
        //public async void InsertNewFuel_ShouldAddIdAndReturnFuel_WhenFuelIsSuccessfullyInserted()
        //{
        //    // Arrange
        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuel = new Fuel()
        //    {
        //        FuelName = "Test"
        //    };

        //    // Act
        //    var result = await _fuelRepository.InsertNewFuel(fuel);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(1, result.Id);
        //    Assert.Equal("Test", result.FuelName);
        //}

        //[Fact]
        //public async void InsertNewFuel_ShouldFailToAddFuel_WhenFuelWithSameIdAlreadyExists()
        //{
        //    // Arrange
        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuel = new Fuel()
        //    {
        //        Id = 1,
        //        FuelName = "Test"
        //    };

        //    _dBContext.Fuel.Add(fuel);

        //    await _dBContext.SaveChangesAsync();

        //    // Act
        //    async Task action() => await _fuelRepository.InsertNewFuel(fuel);

        //    // Assert
        //    var ex = await Assert.ThrowsAsync<ArgumentException>(action);
        //    Assert.Contains("An item with the same key has already been added", ex.Message);
        //}

        //[Fact]
        //public async void UpdateFuelById_ShouldReturnFuel_WhenFuelIsSuccessfullyUpdated()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuel = new Fuel()
        //    {
        //        FuelName = "Test"
        //    };

        //    _dBContext.Fuel.Add(fuel);

        //    await _dBContext.SaveChangesAsync();

        //    Fuel fuelUpdate = new Fuel()
        //    {
        //        FuelName = "Test2"
        //    };

        //    // Act
        //    var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(1, result.Id);
        //    Assert.Equal("Test2", result.FuelName);
        //}

        //[Fact]
        //public async void UpdateFuelById_ShouldReturnNull_WhenFuelToUpdateDoesNotExist()
        //{
        //    // Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuelUpdate = new Fuel()
        //    {
        //        FuelName = "Test2"
        //    };

        //    // Act
        //    var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

        //    // Assert
        //    Assert.Null(result);
        //}

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
