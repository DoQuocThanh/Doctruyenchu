using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IUserService
    {
        Task<LoginViewModel> LoginUser(LoginViewModel loginViewModel); 

    }
}
