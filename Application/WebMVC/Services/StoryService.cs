using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class StoryService : IStoryService
    {
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Story> _storyRepository;

        public StoryService(IRepository<Genre> genreRepository, IRepository<Chapter> chapterRepository, IRepository<Story> storyRepository)
        {
            _genreRepository = genreRepository;
            _chapterRepository = chapterRepository;
            _storyRepository = storyRepository;
        }
        public async Task<StoryDetailViewModel> GetInformationStoryDetail(int id, PageViewModel page) {
            var listStory = await _storyRepository.ListAsync();
            var listGenre = await _genreRepository.ListAsync();
            var listChapter = await _chapterRepository.ListAsync();
            var story = listStory.FirstOrDefault(x => x.Id == id);
            var listChapterViewModel = listChapter.Where(x => x.StoryId == story.Id).Select(x => new ChapterViewModel()
            {
                Content = x.Content,
                Title = x.Title,
                Id = x.Id,
                StoryId = x.Id,
            }).ToList();

            // Lấy thông tin phân trang
            var pageIndex = page.PageIndex ?? 1;
            var pageSize = 50;

            // Tổng số trang
            var totalPages = (int)Math.Ceiling((double)listChapterViewModel.Count / pageSize);

            // Áp dụng logic phân trang
            var pagedChapters = listChapterViewModel
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var half = (int)Math.Ceiling(pagedChapters.Count / 2.0);

            var firstColumn = pagedChapters.Take(half).ToList();
            var secondColumn = pagedChapters.Skip(half).ToList();

            var storyViewModel = new StoryDetailViewModel()
            {
                Title = story.Title,
                View = story.View,
                StoryId = story.Id,
                Status = story.Status,
                Author = story.Author,
                Description = story.Description,
                CoverImage = story.CoverImage,
                Genres = !string.IsNullOrWhiteSpace(story.Genre)
                        ? story.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(id => listGenre.FirstOrDefault(g => g.Id == int.Parse(id)))
                            .Where(g => g != null) // Lọc bỏ null nếu ID không hợp lệ hoặc không tìm thấy
                            .Select(g => new GenreViewModel
                            {
                                Id = g.Id,
                                GenreName = g.Name
                            }).ToList() : new List<GenreViewModel>(),
                ChaptersCloumOne = firstColumn,
                ChaptersCloumTwo = secondColumn,
                CurrentPage = pageIndex,
                TotalPages = totalPages,

            };

            return storyViewModel;
        }



    }
}
