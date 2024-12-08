using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Common;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        // Inject IUserService vào constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                return View(loginViewModel); // Nếu không hợp lệ, trả về view với lỗi
            }

            try
            {
                // Gọi UserService để đăng nhập và lấy token
                var token = await _userService.LoginUser(loginViewModel);

                // Lưu token vào cookie (HttpOnly và Secure cho bảo mật)
                Response.Cookies.Append("JwtToken", token, new CookieOptions
                {
                    HttpOnly = true, // Đảm bảo chỉ có thể truy cập từ phía server, bảo mật hơn
                    Secure = true,   // Đảm bảo chỉ gửi qua HTTPS
                    SameSite = SameSiteMode.Strict, // Hạn chế gửi cookie trong các yêu cầu cross-site
                    Expires = DateTime.UtcNow.AddDays(1) // Token sẽ hết hạn sau 1 ngày
                });

                // Sau khi đăng nhập thành công, chuyển hướng đến trang home hoặc trang khác
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException ex)
            {
                // Xử lý khi thông tin đăng nhập không đúng (Email hoặc mật khẩu sai)
                ModelState.AddModelError("", ex.Message); // Thêm thông báo lỗi vào ModelState
                return View(loginViewModel); // Trả về view với thông báo lỗi
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            try
            {
                await _userService.RegisterUser(registerViewModel);
                TempData["Message"] = "Đăng ký thành công! Hãy đăng nhập.";
                return RedirectToAction("Login", "User");
            }
            catch (UnauthorizedAccessException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerViewModel);
            }
        }

        public IActionResult Logout()
        {
            // Xoá cookie chứa token
            Response.Cookies.Delete("JwtToken");

            // Chuyển hướng về trang đăng nhập hoặc trang chủ
            return RedirectToAction("Login", "User");
        }


        //Profile
        [CustomAuthorize]
        public async Task<IActionResult> Profile()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profileView = await _userService.GetInformationProfile(Int32.Parse(userId));
            return View(profileView);
        }

    }
}
