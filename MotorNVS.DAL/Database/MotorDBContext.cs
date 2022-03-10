using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Database
{
    public class MotorDBContext : DbContext
    {
        public MotorDBContext() { }

        public MotorDBContext(DbContextOptions<MotorDBContext> contextOptions) : base(contextOptions) { }

        DbSet<ZipCode>? ZipCode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZipCode>().HasData(
                new ZipCode()
                {
                    Id = 1,
                    ZipcodeNo = "2450"
                });
        }
    }
}
