using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Database
{
    public class MotorDBContext : DbContext
    {
        public MotorDBContext() { }

        public MotorDBContext(DbContextOptions<MotorDBContext> contextOptions) : base(contextOptions) { }

        public DbSet<Zipcode> Zipcode { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Fuel> Fuel { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Registration> Registration { get; set; }
        public DbSet<Login> Login { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zipcode>().HasData(
                new Zipcode()
                {
                    Id = 1,
                    ZipcodeNo = "2450",
                    City = "København SV"
                },
                new Zipcode()
                {
                    Id = 2,
                    ZipcodeNo = "3300",
                    City = "Frederiksværk"
                });

            modelBuilder.Entity<Address>().HasData(
                new Address()
                {
                    Id = 1,
                    StreetAndNo = "Bakkevej 18",
                    CreateDate = DateTime.Now,
                    ZipCodeId = 2
                },
                new Address()
                {
                    Id = 2,
                    StreetAndNo = "Pladehalebakke 15",
                    CreateDate = DateTime.Now,
                    ZipCodeId = 1
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Nicklas",
                    LastName = "Sams",
                    CreateDate = DateTime.Now,
                    AddressId = 2,
                    IsActive = "yes"
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Henning",
                    LastName = "Bjarkesen",
                    CreateDate = DateTime.Now,
                    AddressId = 1,
                    IsActive = "yes"
                });

            modelBuilder.Entity<Fuel>().HasData(
                new Fuel()
                {
                    Id = 1,
                    FuelName = "Gasoline"
                },
                new Fuel()
                {
                    Id = 2,
                    FuelName = "Diesel"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    CategoryName = "Convertible"
                },
                new Category()
                {
                    Id = 2,
                    CategoryName = "SUV"
                });

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle()
                {
                    Id = 1,
                    Make = "Suzuki",
                    Model = "Vitara",
                    CategoryId = 2,
                    FuelId = 1,
                    CreateDate = DateTime.Now,
                    IsActive = "yes"
                },
                new Vehicle()
                {
                    Id = 2,
                    Make = "Volkswagen",
                    Model = "Beetle Turbo",
                    CategoryId = 1,
                    FuelId = 2,
                    CreateDate = DateTime.Now,
                    IsActive = "yes"
                });

            modelBuilder.Entity<Registration>().HasData(
                new Registration()
                {
                    Id = 1,
                    CustomerId = 1,
                    VehicleId = 2,
                    RegistrationDate = DateTime.Now
                },
                new Registration()
                {
                    Id = 2,
                    CustomerId = 2,
                    VehicleId = 1,
                    RegistrationDate = DateTime.Now
                });

            modelBuilder.Entity<Login>().HasData(
                new Login()
                {
                    Id = 1,
                    Username = "admin",
                    Password = "Passw0rd"
                });
        }
    }
}
