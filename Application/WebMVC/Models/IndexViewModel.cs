namespace WebMVC.Models
{
    public class IndexViewModel
    {
        public ICollection<StoryViewModel> HotStories { get; set; }
        public ICollection<StoryViewModel> NewStories { get; set; }
        public ICollection<GenreViewModel> Genres { get; set; }
        public ICollection<StoryViewModel> Stories { get; set; }

    }
}
