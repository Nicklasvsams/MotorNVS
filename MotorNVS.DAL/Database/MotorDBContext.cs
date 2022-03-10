using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Database
{
    public class MotorDBContext : DbContext
    {
        public MotorDBContext() { }

        public MotorDBContext(DbContextOptions<MotorDBContext> contextOptions) : base(contextOptions) { }

        DbSet<Zipcode> Zipcode { get; set; }
        DbSet<Address> Address { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Fuel> Fuel { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Vehicle> Vehicle { get; set; }
        DbSet<Registration> Registration { get; set; }
        DbSet<Login> Login { get; set; }
 
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
                    CreateDate = DateTimeOffset.Now,
                    ZipCodeId = 2
                },
                new Address()
                {
                    Id = 2,
                    StreetAndNo = "Pladehalebakke 15",
                    CreateDate = DateTimeOffset.Now,
                    ZipCodeId = 1
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    FirstName = "Nicklas",
                    LastName = "Sams",
                    CreateDate = DateTimeOffset.Now,
                    AddressId = 2
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Henning",
                    LastName = "Bjarkesen",
                    CreateDate = DateTimeOffset.Now,
                    AddressId = 1
                });

            // TODO: Add more testdata
        }
    }
}
