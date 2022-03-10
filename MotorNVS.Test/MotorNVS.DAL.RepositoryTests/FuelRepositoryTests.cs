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
    public class FuelRepositoryTests
    {
        private readonly DbContextOptions<MotorDBContext> _options;
        private readonly MotorDBContext _dBContext;
        private readonly FuelRepository _fuelRepository;

        public FuelRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<MotorDBContext>()
                .UseInMemoryDatabase(databaseName: "MotorFuel")
                .Options;
            _dBContext = new MotorDBContext(_options);
            _fuelRepository = new FuelRepository(_dBContext);
        }

        [Fact]
        public async void GetAllFuels_ReturnsListOfFuels_WhenFuelsExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.AddRange(FuelList());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _fuelRepository.GetAllFuels();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Fuel>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllFuels_ReturnsEmptyList_WhenFuelsDoNotExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _fuelRepository.GetAllFuels();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Fuel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetFuelById_ReturnsSingleFuel_WhenFuelIdExists()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.Add(Fuel());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _fuelRepository.GetFuelById(fuelId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Fuel>(result);
            Assert.Equal(fuelId, result.Id);
        }

        [Fact]
        public async void GetFuelById_ReturnsNull_WhenFuelIdDoesNotExist()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _fuelRepository.GetFuelById(fuelId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteFuelById_ReturnsFuel_WhenFuelToDeleteExists()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.Add(Fuel());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _fuelRepository.DeleteFuelById(fuelId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Fuel>(result);
            Assert.Equal(fuelId, result.Id);
        }

        [Fact]
        public async void DeleteFuelById_ReturnsNull_WhenFuelIdDoesNotExist()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _fuelRepository.DeleteFuelById(fuelId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewFuel_ShouldAddIdAndReturnFuel_WhenFuelIsSuccessfullyInserted()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            Fuel fuel = new Fuel()
            {
                FuelName = "Test"
            };

            // Act
            var result = await _fuelRepository.InsertNewFuel(fuel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Fuel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.FuelName);
        }

        [Fact]
        public async void InsertNewFuel_ShouldFailToAddFuel_WhenFuelWithSameIdAlreadyExists()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            Fuel fuel = new Fuel()
            {
                Id = 1,
                FuelName = "Test"
            };

            _dBContext.Fuel.Add(fuel);

            await _dBContext.SaveChangesAsync();

            // Act
            async Task action() => await _fuelRepository.InsertNewFuel(fuel);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateFuelById_ShouldReturnFuel_WhenFuelIsSuccessfullyUpdated()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            Fuel fuel = new Fuel()
            {
                FuelName = "Test"
            };

            _dBContext.Fuel.Add(fuel);

            await _dBContext.SaveChangesAsync();

            Fuel fuelUpdate = new Fuel()
            {
                FuelName = "Test2"
            };

            // Act
            var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Fuel>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test2", result.FuelName);
        }

        [Fact]
        public async void UpdateFuelById_ShouldReturnNull_WhenFuelToUpdateDoesNotExist()
        {
            // Arrange
            int fuelId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            Fuel fuelUpdate = new Fuel()
            {
                FuelName = "Test2"
            };

            // Act
            var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

            // Assert
            Assert.Null(result);
        }

        private List<Fuel> FuelList()
        {
            return new List<Fuel>()
            {
                new Fuel()
                {
                    Id = 1,
                    FuelName = "Test"
                },
                new Fuel()
                {
                    Id = 2,
                    FuelName = "Test2"
                }
            };
        }

        private Fuel Fuel()
        {
            return new Fuel()
                {
                    Id = 1,
                    FuelName = "Test"
                };
        }
    }
}
