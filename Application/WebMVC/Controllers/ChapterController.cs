using Microsoft.AspNetCore.Mvc;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ChapterController : Controller
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        public async Task<IActionResult> ChapterDetail(int chapterId, int storyId)
        {
            try
            {
                var chapterViewModel = await _chapterService.GetChapterByIdAsync(chapterId,storyId);

                return View(chapterViewModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
