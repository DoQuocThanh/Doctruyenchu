using Microsoft.AspNetCore.Mvc;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class StoryController : Controller
    {

        private readonly IStoryService _storyService;


        public StoryController(IStoryService storyService) {
            _storyService = storyService;
        }
        public async Task<IActionResult> StoryDetail(int id, PageViewModel page)
        {
            var storyViewModel = await _storyService.GetInformationStoryDetail(id,page);
            ViewBag.Filter = page;
            return View(storyViewModel);
        }


    }
}
