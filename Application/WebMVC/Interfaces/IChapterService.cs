using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IChapterService
    {
        Task<ChapterViewModel> GetChapterByIdAsync(int chapterId, int storyId);
    }
}
