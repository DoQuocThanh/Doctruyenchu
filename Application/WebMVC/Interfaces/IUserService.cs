using ApplicationCore.Entities;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginUser(LoginViewModel loginViewModel);
        Task<RegisterViewModel> RegisterUser(RegisterViewModel registerViewModel);

        Task<ProfileViewModel> GetInformationProfile(int id);
    }
}
