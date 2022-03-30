using Microsoft.EntityFrameworkCore;
using MotorNVS.DAL.Database;
using MotorNVS.DAL.Database.Entities;

namespace MotorNVS.DAL.Repositories
{
    public interface ILoginRepository
    {
        Task<Login> GetLoginByName(string name);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly MotorDBContext _dBContext;

        public LoginRepository(MotorDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Login> GetLoginByName(string name)
        {
            return await _dBContext.Login.FirstOrDefaultAsync(x => x.Username == name);
        }
    }
}