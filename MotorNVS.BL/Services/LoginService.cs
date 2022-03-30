using MotorNVS.BL.DTOs.LoginDTO;
using MotorNVS.DAL.Database.Entities;
using MotorNVS.DAL.Repositories;

namespace MotorNVS.BL.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> AuthorizeLogin(string username, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginResponse> AuthorizeLogin(string username, string password)
        {
            Login login = await _loginRepository.GetLoginByName(username);
            LoginResponse logRes = new LoginResponse();

            if(login != null)
            {
                if(login.Username == username && login.Password == password)
                {
                    logRes.LoginAuthorized = true;
                }
                else
                {
                    logRes.LoginAuthorized = false;
                }

                return logRes;
            }

            return null;
        }
    }
}
