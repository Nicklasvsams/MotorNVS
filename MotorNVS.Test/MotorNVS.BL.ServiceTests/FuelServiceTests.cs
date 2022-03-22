using Moq;
using MotorNVS.BL.DTOs.FuelDTO;
using MotorNVS.BL.Services;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;
using System.Collections.Generic;
using Xunit;

namespace MotorNVS.Test.MotorNVS.BL.ServiceTests
{
    public class FuelServiceTests
    {
        private readonly FuelService _fuelService;
        private readonly Mock<IFuelRepository> _mockFuelRepository = new Mock<IFuelRepository>();

        public FuelServiceTests()
        {
            _fuelService = new FuelService(_mockFuelRepository.Object);
        }

        [Fact]
        public async void GetAllFuels_ShouldReturnListOfFuelResponse_WhenFuelsExist()
        {
            // Arrange
            _mockFuelRepository
                .Setup(x => x.SelectAllFuels())
                .ReturnsAsync(FuelList());

            // Act
            var result = await _fuelService.GetAllFuels();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FuelResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllFuels_ShouldReturnEmptyListOfFuelResponses_WhenNoFuelsExist()
        {
            // Arrange
            List<Fuel> emptyFuelList = new List<Fuel>();

            _mockFuelRepository
                .Setup(x => x.SelectAllFuels())
                .ReturnsAsync(emptyFuelList);

            // Act
            var result = await _fuelService.GetAllFuels();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FuelResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetFuelById_ShouldReturnSingleFuelResponse_WhenFuelExists()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.SelectFuelById(It.IsAny<int>()))
                .ReturnsAsync(Fuel());

            // Act
            var result = await _fuelService.GetFuelById(fuelId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FuelResponse>(result);
            Assert.Equal(fuelId, result.Id);
            Assert.Equal("Test", result.FuelName);
        }

        [Fact]
        public async void GetFuelById_ShouldReturnNull_WhenFuelDoesNotExist()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.SelectFuelById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _fuelService.GetFuelById(fuelId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteFuelById_ShouldReturnFuelResponse_WhenFuelToDeleteExists()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.DeleteFuelById(It.IsAny<int>()))
                .ReturnsAsync(Fuel());

            // Act
            var result = await _fuelService.DeleteFuelById(fuelId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FuelResponse>(result);
            Assert.Equal(fuelId, result.Id);
        }

        [Fact]
        public async void DeleteFuelById_ShouldReturnNull_WhenFuelToDeleteDoesNotExist()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.DeleteFuelById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _fuelService.DeleteFuelById(fuelId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateFuel_ShouldReturnFuelResponse_WhenFuelIsSuccessfullyCreated()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.InsertNewFuel(It.IsAny<Fuel>()))
                .ReturnsAsync(Fuel());

            // Act
            var result = await _fuelService.CreateFuel(FuelRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FuelResponse>(result);
            Assert.Equal(fuelId, result.Id);
        }

        [Fact]
        public async void CreateFuel_ShouldReturnNull_WhenFuelIsNotCreated()
        {
            // Arrange
            _mockFuelRepository
                .Setup(x => x.InsertNewFuel(It.IsAny<Fuel>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _fuelService.CreateFuel(FuelRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateFuel_ShouldReturnFuelResponse_WhenFuelIsSuccessfullyUpdated()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.UpdateFuelById(It.IsAny<int>(), It.IsAny<Fuel>()))
                .ReturnsAsync(Fuel());

            // Act
            var result = await _fuelService.UpdateFuel(fuelId, FuelRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FuelResponse>(result);
            Assert.Equal(fuelId, result.Id);
        }

        [Fact]
        public async void UpdateFuel_ShouldReturnNull_WhenFuelIsNotUpdated()
        {
            // Arrange
            int fuelId = 1;

            _mockFuelRepository
                .Setup(x => x.UpdateFuelById(It.IsAny<int>(), It.IsAny<Fuel>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _fuelService.UpdateFuel(fuelId, FuelRequest());

            // Assert
            Assert.Null(result);
        }

        private static List<Fuel> FuelList()
        {
            return new List<Fuel>()
            {
                new Fuel(){ 
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

        private static Fuel Fuel()
        {
            return new Fuel()
            {
                Id = 1,
                FuelName = "Test"
            };
        }

        private static FuelRequest FuelRequest()
        {
            return new FuelRequest()
            {
                FuelName = "Test"
            };
        }
    }
}
