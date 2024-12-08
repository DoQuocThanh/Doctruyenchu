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
        private readonly IRepository<Genre> _genreRepository;

        public UserService(IRepository<User> userRepository, IRepository<Genre> genreRepository, GenerateJwtToken generateJwtToken)
        {
            _userRepository = userRepository;
            _generateJwtToken = generateJwtToken;
            _genreRepository = genreRepository;
        }
        public async Task<string> LoginUser(LoginViewModel loginViewModel)
        {

            var userSpec = new UserWithItemsSpecification(loginViewModel.Email);
            var userList = await _userRepository.ListAsync(userSpec);
            var user = userList.FirstOrDefault();

            if (user == null || !PasswordHasher.VerifyPassword(loginViewModel.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Email hoặc mật khẩu không đúng");
            }

            string token = _generateJwtToken.GenerateJwtTokenUser(user.Id.ToString(), user.Username, user.Email);


            return token;
        }

        public async Task<RegisterViewModel> RegisterUser(RegisterViewModel registerViewModel)
        {
            var userSpec = new UserWithItemsSpecification(registerViewModel.Email);
            var userList = await _userRepository.ListAsync(userSpec);
            var user = userList.FirstOrDefault();
            if (user != null)
            {
                throw new UnauthorizedAccessException("Email này đã tồn tại");
            }
            else
            {
                user = new User
                {

                    Username = registerViewModel.Username,
                    Email = registerViewModel.Email,
                    PasswordHash = PasswordHasher.HashPassword(registerViewModel.Password)

                };

                await _userRepository.AddAsync(user);

            }

            return registerViewModel;

        }

        public async Task<ProfileViewModel> GetInformationProfile(int id)
        {


            var userSpec = new UserWithItemsSpecification(id);
            var userList = await _userRepository.ListAsync(userSpec);
            var user = userList.FirstOrDefault();

            var listGenre = await _genreRepository.ListAsync();


            var profileView = new ProfileViewModel
            {
                Stories = user.Stories.Select(x => new StoryViewModel()
                {
                    CoverImage = x.CoverImage,
                    CreatedAt = x.CreatedAt,
                    Description = x.Description,
                    UpdatedAt = x.UpdatedAt,
                    Status = x.Status,
                    Title = x.Title,
                    StoryId = x.Id,
                    Genres = !string.IsNullOrWhiteSpace(x.Genre)
                    ? x.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => listGenre.FirstOrDefault(g => g.Id == int.Parse(id)))
                        .Where(g => g != null) // Lọc bỏ null nếu ID không hợp lệ hoặc không tìm thấy
                        .Select(g => new GenreViewModel
                        {
                            Id = g.Id,
                            GenreName = g.Name
                        }).ToList() : new List<GenreViewModel>()
                    }).ToList(),



                Email = user.Email,
                Name = user.Username,
                NumberStoriesWritten = user.Stories.Count.ToString(),
            };



            return profileView;
        }


    }
}
