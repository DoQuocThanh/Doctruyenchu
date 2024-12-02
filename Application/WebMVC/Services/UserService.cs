using ApplicationCore.Common;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using WebMVC.Common;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly GenerateJwtToken _generateJwtToken;

        public UserService(IRepository<User> userRepository, GenerateJwtToken generateJwtToken) {
            _userRepository = userRepository;
            _generateJwtToken = generateJwtToken;
        }
        public async Task<string> LoginUser(LoginViewModel loginViewModel) {

            var userSpec = new UserWithItemsSpecification(loginViewModel.Email);
            var userList = await _userRepository.ListAsync(userSpec);
            var user = userList.FirstOrDefault();

            if (user == null || !PasswordHasher.VerifyPassword(loginViewModel.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng");
            }

            string token = _generateJwtToken.GenerateJwtTokenUser(user.Id.ToString(),user.Username,user.Email);


            return token;
        }
    }
}
