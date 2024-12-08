using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Models;
using WebMVC.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImportService _import;
        public HomeController(ILogger<HomeController> logger, ImportService import)
        {
            _logger = logger;
            _import = import;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
