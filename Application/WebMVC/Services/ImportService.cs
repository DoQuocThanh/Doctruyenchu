using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Humanizer.Localisation;
using System.Text.Json;
using WebMVC.Common;

namespace WebMVC.Services
{
    public class StoryJson
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; } // "Khoa Huyễn, Điềm Đạm, Thời Không Xuyên Toa"
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int View { get; set; }
        public ICollection<ChapterJson> Chapters { get; set; }
    }

    public class ChapterJson
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class ImportService
    {
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Story> _storyRepository;
        private readonly IRepository<Genre> _genreRepository;
        public ImportService(IRepository<Chapter> chapterRepository, IRepository<Story> storyRepository, IRepository<Genre> genreRepository)
        {
            _chapterRepository = chapterRepository;
            _storyRepository = storyRepository;
            _genreRepository = genreRepository;
        }

        public async Task ImportStoriesFromJsonAsync(string jsonFilePath, int userId)
        {
            // 1. Đọc JSON từ file
            var jsonData = File.ReadAllText(jsonFilePath);
            var listStory = JsonSerializer.Deserialize<List<StoryJson>>(jsonData);

            foreach (var storyJson in listStory)
            {
                if (storyJson == null) throw new Exception("Không có dữ liệu JSON hợp lệ!");

                // 2. Lưu các thể loại (Genre)
                var genreNames = storyJson.Genre.Split(',').Select(g => g.Trim()).ToList();
                var genreIds = new List<int>();

                foreach (var genreName in genreNames)
                {
                    var listGenre = await _genreRepository.ListAsync();
                    var checkGenre = listGenre.FirstOrDefault(x => x.Name == genreName);
                    if (!(checkGenre != null))
                    {
                        var genre = new Genre { Name = genreName };

                        await _genreRepository.AddAsync(genre);
                        await _genreRepository.SaveChangesAsync(); // Lưu để lấy Id
                        genreIds.Add(genre.Id);
                    }
                    else {
                        genreIds.Add(checkGenre.Id);
                    }

                }

                // 3. Lưu Story
                var story = new Story
                {
                    Title = storyJson.Title,
                    Author = storyJson.Author,
                    CoverImage = storyJson.CoverImage,
                    Status = storyJson.Status,
                    Genre = string.Join(",", genreIds), // Lưu các ID thể loại dạng chuỗi
                    Description = storyJson.Description,
                    CreatedAt = storyJson.CreatedAt,
                    UserId = userId,
                    UpdatedAt = null,
                    Chapters = new List<Chapter>(), 
                    View = storyJson.View,
                };

                await _storyRepository.AddAsync(story);
                await _storyRepository.SaveChangesAsync();

                // 4. Lưu Chapter liên kết với Story
                foreach (var chapterJson in storyJson.Chapters)
                {
                    var chapter = new Chapter
                    {
                        Title = chapterJson.Title,
                        Content = chapterJson.Content,
                        CreatedAt = chapterJson.CreatedAt,
                        UpdatedAt = null,
                        StoryId = story.Id
                    };

                    await _chapterRepository.AddAsync(chapter);
                }

                await _chapterRepository.SaveChangesAsync();
            }
        }
           
        


    }
}
