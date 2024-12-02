using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginUser(LoginViewModel loginViewModel); 

    }
}
