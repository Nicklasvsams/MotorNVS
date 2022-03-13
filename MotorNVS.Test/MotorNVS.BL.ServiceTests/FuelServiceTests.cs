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
