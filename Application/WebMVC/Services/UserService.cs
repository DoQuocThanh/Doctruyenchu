using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public async Task<LoginViewModel> LoginUser(LoginViewModel loginViewModel) {

            var userSpec = new UserWithItemsSpecification(loginViewModel.Email);
            var userList = await _userRepository.ListAsync(userSpec);
            var user = userList.FirstOrDefault();

            if (user == null || !PasswordHasher.VerifyPassword(loginViewModel.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng");
            }



            return loginViewModel;
        }
    }
}
