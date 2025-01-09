using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class ChapterService : IChapterService
    {

        private readonly IRepository<Chapter> _chapterRepository;

        public ChapterService(IRepository<Chapter> chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }
        public async Task<ChapterViewModel> GetChapterByIdAsync(int chapterId, int storyId)
        {
            try
            {
                var chapterSpec = new ChapterWithItemsSpecification(storyId);
                var listChapterStory = await _chapterRepository.ListAsync(chapterSpec);
                var chapter = listChapterStory.FirstOrDefault(x => x.Id == chapterId);

                if (chapter == null)
                {
                    throw new KeyNotFoundException("Chapter not found");
                }

                var currentIndex = listChapterStory.FindIndex(c => c.Id == chapterId);

                if (currentIndex == null) throw new KeyNotFoundException("Chapter not found");

                var previousChapter = currentIndex > 0 ? listChapterStory[currentIndex - 1] : null;

                var nextChapter = currentIndex < listChapterStory.Count - 1 ? listChapterStory[currentIndex + 1] : null;

                // Map sang ViewModel
                return new ChapterViewModel
                {
                    Id = chapter.Id,
                    StoryId = chapter.StoryId,
                    Title = chapter.Title,
                    Content = chapter.Content,
                    CreatedAt = chapter.CreatedAt,
                    UpdatedAt = chapter.UpdatedAt,
                    PreviousChapterId = previousChapter?.Id,
                    NextChapterId = nextChapter?.Id
                };

            }
            catch (Exception)
            {

                throw new KeyNotFoundException("Chapter not found");
            }
            

        }

        
    }
}
