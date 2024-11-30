using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
