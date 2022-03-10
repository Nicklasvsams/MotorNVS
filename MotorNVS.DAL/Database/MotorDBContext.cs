using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Database
{
    public class MotorDBContext : DbContext
    {
        public MotorDBContext() { }

        public MotorDBContext(DbContextOptions<MotorDBContext> contextOptions) : base(contextOptions) { }
    }
}
