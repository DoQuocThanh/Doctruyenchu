using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebMVC.Interfaces;

namespace WebMVC.Services
{
    public class StoryService : IStoryService
    {
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        private readonly IRepository<Story> _storyRepository;

        public StoryService(IRepository<Genre> genreRepository, IRepository<Chapter> chapterRepository) {
        
        }
    }
}
