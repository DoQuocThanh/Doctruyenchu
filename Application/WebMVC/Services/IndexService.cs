using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebMVC.Interfaces;
using WebMVC.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebMVC.Services
{
    public class IndexService : IIndexService
    {

        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Story> _storyRepository;
        private readonly IRepository<Genre> _genreRepository;

        public IndexService(IRepository<Chapter> chapterRepository, IRepository<Story> storyRepository, IRepository<Genre> genreRepository)
        {
            _chapterRepository = chapterRepository;
            _storyRepository = storyRepository;
            _genreRepository = genreRepository;
        }

        public async Task<IndexViewModel> GetInformationIndex()
        {
            var listStory = await _storyRepository.ListAsync();
            var listGenre = await _genreRepository.ListAsync();
            var indexViewModel = new IndexViewModel
            {
                HotStories = listStory.OrderByDescending(x => x.View).Take(5).Select(x => new StoryViewModel()
                {
                    Title = x.Title,
                    View = x.View,
                    StoryId = x.Id,
                    Status = x.Status,
                    CoverImage = x.CoverImage,
                }).ToList(),
                NewStories = listStory.OrderBy(x => x.UpdatedAt).Take(9).Select(x => new StoryViewModel()
                {
                    Title = x.Title,
                    View = x.View,
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
                Genres = listGenre.Select(x => new GenreViewModel() { Id = x.Id, GenreName = x.Name }).ToList()
            };

            return indexViewModel;
        }
        public async Task<IndexFilterViewModel> GetInformationIndexFilter(FilterViewModel filterViewModel)
        {
            var listStory = await _storyRepository.ListAsync();
            var listGenre = await _genreRepository.ListAsync();
       
            // Lọc theo Title (nếu có)
            if (!string.IsNullOrEmpty(filterViewModel.Title))
            {
                listStory = listStory.Where(x => x.Title.Contains(filterViewModel.Title, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Lọc theo Genres (nếu có)
            if (!string.IsNullOrEmpty(filterViewModel.GenresSlected))
            {
                var genreIds = filterViewModel.GenresSlected.Split(',').Select(id => id.Trim()).ToList();
                listStory = listStory.Where(x => genreIds.Any(genreId => x.Genre.Split(',').Contains(genreId))).ToList();
            }

            // Lấy thông tin phân trang
            var pageIndex = filterViewModel.PageIndex ?? 1;
            var pageSize = filterViewModel.PageSize ?? 15;
            int number = listStory.Count;

            // Tổng số trang
            var totalPages = (int)Math.Ceiling((double)number / pageSize);

            // Áp dụng logic phân trang
            var pagedStories = listStory
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var indexViewModel = new IndexFilterViewModel
            {
                NumberStories = number,
                CurrentPage = pageIndex,
                TotalPages = totalPages,
                Stories = pagedStories.Select(x => new StoryViewModel
                {
                    Title = x.Title,
                    View = x.View,
                    StoryId = x.Id,
                    Status = x.Status,
                    CoverImage = x.CoverImage,
                }).ToList(),
                Genres = listGenre.Select(x => new GenreViewModel
                {
                    Id = x.Id,
                    GenreName = x.Name
                }).ToList(),
            };

            return indexViewModel;
        }



    }
}
