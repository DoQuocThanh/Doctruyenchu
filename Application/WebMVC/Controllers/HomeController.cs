using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Interfaces;
using WebMVC.Models;
using WebMVC.Services;
using static System.Net.Mime.MediaTypeNames;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImportService _import;
        private readonly IIndexService _indexService;
        //await _import.ImportStoriesFromJsonAsync("D:\\DoctruyenchuProject\\Application\\WebMVC\\StoryFile\\danhsachtruyen.txt", 1);
        public HomeController(ILogger<HomeController> logger, ImportService import, IIndexService indexService )
        {
            _logger = logger;
            _import = import;
            _indexService = indexService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var indexViewModel = await _indexService.GetInformationIndex();
            return View(indexViewModel);
        }

        public async Task<IActionResult> IndexFilterAsync(FilterViewModel filterViewModel)
        {
            var indexFilterViewModel = await _indexService.GetInformationIndexFilter(filterViewModel);

            // Pass filter model to view for further paging actions
          
            ViewBag.Filter = filterViewModel;

            return View(indexFilterViewModel);
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
