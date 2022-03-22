using Moq;
using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.DTOs.RegistrationDTO;
using MotorNVS.BL.DTOs.VehicleDTO;
using MotorNVS.BL.Services;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace MotorNVS.Test.MotorNVS.BL.ServiceTests
{
    public class RegistrationServiceTests
    {
        private readonly RegistrationService _registrationService;
        private readonly Mock<IRegistrationRepository> _mockRegistrationRepository = new Mock<IRegistrationRepository>();

        public RegistrationServiceTests()
        {
            _registrationService = new RegistrationService(_mockRegistrationRepository.Object);
        }

        [Fact]
        public async void GetAllRegistrations_ShouldReturnListOfRegistrationResponses_WhenRegistrationsExist()
        {
            // Arrange
            _mockRegistrationRepository
                .Setup(x => x.SelectAllRegistrations())
                .ReturnsAsync(RegistrationList());

            // Act
            var result = await _registrationService.GetAllRegistrations();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<RegistrationResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllRegistrations_ShouldReturnEmptyListOfRegistrationResponses_WhenNoRegistrationsExist()
        {
            // Arrange
            List<Registration> emptyRegistrationList = new List<Registration>();

            _mockRegistrationRepository
                .Setup(x => x.SelectAllRegistrations())
                .ReturnsAsync(emptyRegistrationList);

            // Act
            var result = await _registrationService.GetAllRegistrations();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<RegistrationResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetRegistrationById_ShouldReturnSingleRegistrationResponse_WhenRegistrationExists()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.SelectRegistrationById(It.IsAny<int>()))
                .ReturnsAsync(Registration());

            // Act
            var result = await _registrationService.GetRegistrationById(registrationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationResponse>(result);
            Assert.Equal(registrationId, result.Id);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(1, result.VehicleId);
            Assert.IsType<VehicleResponse>(result.VehicleResponse);
            Assert.IsType<CustomerResponse>(result.CustomerResponse);
        }

        [Fact]
        public async void GetRegistrationById_ShouldReturnNull_WhenRegistrationDoesNotExist()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.SelectRegistrationById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _registrationService.GetRegistrationById(registrationId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteRegistrationById_ShouldReturnRegistrationResponse_WhenRegistrationToDeleteExists()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.DeleteRegistrationById(It.IsAny<int>()))
                .ReturnsAsync(Registration());

            // Act
            var result = await _registrationService.DeleteRegistrationById(registrationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationResponse>(result);
            Assert.Equal(registrationId, result.Id);
        }

        [Fact]
        public async void DeleteRegistrationById_ShouldReturnNull_WhenRegistrationToDeleteDoesNotExist()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.DeleteRegistrationById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _registrationService.DeleteRegistrationById(registrationId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateRegistration_ShouldReturnRegistrationResponse_WhenRegistrationIsSuccessfullyCreated()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.InsertNewRegistration(It.IsAny<Registration>()))
                .ReturnsAsync(Registration());

            // Act
            var result = await _registrationService.CreateRegistration(RegistrationRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationResponse>(result);
            Assert.Equal(registrationId, result.Id);
        }

        [Fact]
        public async void CreateRegistration_ShouldReturnNull_WhenRegistrationIsNotCreated()
        {
            // Arrange
            _mockRegistrationRepository
                .Setup(x => x.InsertNewRegistration(It.IsAny<Registration>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _registrationService.CreateRegistration(RegistrationRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateRegistration_ShouldReturnRegistrationResponse_WhenRegistrationIsSuccessfullyUpdated()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.UpdateRegistrationById(It.IsAny<int>(), It.IsAny<Registration>()))
                .ReturnsAsync(Registration());

            // Act
            var result = await _registrationService.UpdateRegistration(registrationId, RegistrationRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RegistrationResponse>(result);
            Assert.Equal(registrationId, result.Id);
        }

        [Fact]
        public async void UpdateRegistration_ShouldReturnNull_WhenRegistrationIsNotUpdated()
        {
            // Arrange
            int registrationId = 1;

            _mockRegistrationRepository
                .Setup(x => x.UpdateRegistrationById(It.IsAny<int>(), It.IsAny<Registration>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _registrationService.UpdateRegistration(registrationId, RegistrationRequest());

            // Assert
            Assert.Null(result);
        }

        private static List<Registration> RegistrationList()
        {
            return new List<Registration>()
            {
                new Registration(){
                    Id = 1,
                    RegistrationDate = DateTime.Now,
                    CustomerId = 1,
                    VehicleId = 1,
                    Customer = new Customer()
                    {
                        Id = 1,
                        CreateDate = DateTime.Now,
                        FirstName = "Test",
                        LastName = "Test",
                        AddressId = 1,
                        Address = new Address()
                        {
                            Id = 1,
                            CreateDate = DateTime.Now,
                            StreetAndNo = "Test",
                            ZipCodeId = 1,
                            Zipcode = new Zipcode()
                            {
                                Id = 1,
                                ZipcodeNo = "Test",
                                City = "Test"
                            }
                        }
                    },
                    Vehicle = new Vehicle()
                    {
                        Id = 1,
                        CreateDate = DateTime.Now,
                        Make = "Test",
                        Model = "Test",
                        CategoryId = 1,
                        FuelId = 1,
                        Fuel = new Fuel()
                        {
                            Id = 1,
                            FuelName = "Test"
                        },
                        Category = new Category()
                        {
                            Id = 1,
                            CategoryName = "Test"
                        }
                    }
                },
                new Registration()
                {
                    Id = 2,
                    RegistrationDate = DateTime.Now,
                    CustomerId = 2,
                    VehicleId = 2,
                    Customer = new Customer()
                    {
                        Id = 2,
                        CreateDate = DateTime.Now,
                        FirstName = "Test2",
                        LastName = "Test2",
                        AddressId = 2,
                        Address = new Address()
                        {
                            Id = 2,
                            CreateDate = DateTime.Now,
                            StreetAndNo = "Test2",
                            ZipCodeId = 2,
                            Zipcode = new Zipcode()
                            {
                                Id = 2,
                                ZipcodeNo = "Test2",
                                City = "Test2"
                            }
                        }
                    },
                    Vehicle = new Vehicle()
                    {
                        Id = 2,
                        CreateDate = DateTime.Now,
                        Make = "Test2",
                        Model = "Test2",
                        CategoryId = 2,
                        FuelId = 2,
                        Fuel = new Fuel()
                        {
                            Id = 2,
                            FuelName = "Test2"
                        },
                        Category = new Category()
                        {
                            Id = 2,
                            CategoryName = "Test2"
                        }
                    }
                }
            };
        }

        private static Registration Registration()
        {
            return new Registration()
            {
                Id = 1,
                RegistrationDate = DateTime.Now,
                CustomerId = 1,
                VehicleId = 1,
                Customer = new Customer()
                {
                    Id = 2,
                    CreateDate = DateTime.Now,
                    FirstName = "Test",
                    LastName = "Test",
                    AddressId = 2,
                    Address = new Address()
                    {
                        Id = 1,
                        CreateDate = DateTime.Now,
                        StreetAndNo = "Test",
                        ZipCodeId = 2,
                        Zipcode = new Zipcode()
                        {
                            Id = 2,
                            ZipcodeNo = "Test",
                            City = "Test"
                        }
                    }
                },
                Vehicle = new Vehicle()
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    Make = "Test",
                    Model = "Test",
                    CategoryId = 1,
                    FuelId = 1,
                    Fuel = new Fuel()
                    {
                        Id = 1,
                        FuelName = "Test"
                    },
                    Category = new Category()
                    {
                        Id = 1,
                        CategoryName = "Test"
                    }
                }
            };
        }

        private static RegistrationRequest RegistrationRequest()
        {
            return new RegistrationRequest()
            {
                RegistrationDate = DateTime.Now,
                CustomerId = 1,
                VehicleId = 1
            };
        }
    }
}