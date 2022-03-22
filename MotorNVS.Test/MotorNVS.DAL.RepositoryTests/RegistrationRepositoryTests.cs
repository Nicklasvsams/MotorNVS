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
    public class RegistrationRepositoryTests
    {
        private readonly DbContextOptions<MotorDBContext> _options;
        private readonly MotorDBContext _dBContext;
        private readonly RegistrationRepository _registrationRepository;

        public RegistrationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<MotorDBContext>()
                .UseInMemoryDatabase(databaseName: "MotorFuel")
                .Options;
            _dBContext = new MotorDBContext(_options);
            _registrationRepository = new RegistrationRepository(_dBContext);
        }

        [Fact]
        public async void GetAllRegistrations_ReturnsListOfRegistrations_WhenRegistrationsExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.AddRange(RegistrationList());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _registrationRepository.SelectAllRegistrations();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Registration>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllFuels_ReturnsEmptyList_WhenFuelsDoNotExist()
        {
            // Arrange
            await _dBContext.Database.EnsureDeletedAsync();

            // Act
            var result = await _registrationRepository.SelectAllRegistrations();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Registration>>(result);
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
        //    Assert.Equal("Test", result.FuelName);
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

        [Fact]
        public async void DeleteFuelById_ReturnsFuel_WhenFuelToDeleteExists()
        {
            // Arrange
            int registrationId = 1;

            await _dBContext.Database.EnsureDeletedAsync();

            _dBContext.Add(Registration());

            await _dBContext.SaveChangesAsync();

            // Act
            var result = await _registrationRepository.DeleteRegistrationById(registrationId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Registration>(result);
            Assert.Equal(registrationId, result.Id);
        }

        //[Fact]
        //public async void DeleteFuelById_ReturnsNull_WhenFuelIdDoesNotExist()
        //{
        //     Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //     Act
        //    var result = await _fuelRepository.DeleteFuelById(fuelId);

        //     Assert
        //    Assert.Null(result);
        //}

        //[Fact]
        //public async void InsertNewFuel_ShouldAddIdAndReturnFuel_WhenFuelIsSuccessfullyInserted()
        //{
        //     Arrange
        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuel = new Fuel()
        //    {
        //        FuelName = "Test"
        //    };

        //     Act
        //    var result = await _fuelRepository.InsertNewFuel(fuel);

        //     Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(1, result.Id);
        //    Assert.Equal("Test", result.FuelName);
        //}

        //[Fact]
        //public async void InsertNewFuel_ShouldFailToAddFuel_WhenFuelWithSameIdAlreadyExists()
        //{
        //     Arrange
        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuel = new Fuel()
        //    {
        //        Id = 1,
        //        FuelName = "Test"
        //    };

        //    _dBContext.Fuel.Add(fuel);

        //    await _dBContext.SaveChangesAsync();

        //     Act
        //    async Task action() => await _fuelRepository.InsertNewFuel(fuel);

        //     Assert
        //    var ex = await Assert.ThrowsAsync<ArgumentException>(action);
        //    Assert.Contains("An item with the same key has already been added", ex.Message);
        //}

        //[Fact]
        //public async void UpdateFuelById_ShouldReturnFuel_WhenFuelIsSuccessfullyUpdated()
        //{
        //     Arrange
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

        //     Act
        //    var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

        //     Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<Fuel>(result);
        //    Assert.Equal(1, result.Id);
        //    Assert.Equal("Test2", result.FuelName);
        //}

        //[Fact]
        //public async void UpdateFuelById_ShouldReturnNull_WhenFuelToUpdateDoesNotExist()
        //{
        //     Arrange
        //    int fuelId = 1;

        //    await _dBContext.Database.EnsureDeletedAsync();

        //    Fuel fuelUpdate = new Fuel()
        //    {
        //        FuelName = "Test2"
        //    };

        //     Act
        //    var result = await _fuelRepository.UpdateFuelById(fuelId, fuelUpdate);

        //     Assert
        //    Assert.Null(result);
        //}

        private List<Registration> RegistrationList()
        {
            return new List<Registration>()
            {
                new Registration()
            {
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
                    Id = 2,
                    CreateDate = DateTime.Now,
                    Make = "Test",
                    Model = "Test",
                    CategoryId = 2,
                    FuelId = 2,
                    Fuel = new Fuel()
                    {
                        Id = 2,
                        FuelName = "Test"
                    },
                    Category = new Category()
                    {
                        Id = 2,
                        CategoryName = "Test"
                    }
                }
            }
            };
        }

        private Registration Registration()
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
    }
}
