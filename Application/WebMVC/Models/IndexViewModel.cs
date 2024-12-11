namespace WebMVC.Models
{
    public class IndexViewModel
    {
        public ICollection<StoryViewModel> HotStories { get; set; }
        public ICollection<StoryViewModel> NewStories { get; set; }
        public ICollection<GenreViewModel> Genres { get; set; }
        public ICollection<StoryViewModel> Stories { get; set; }
        public int? NumberStories { get; set; } // Tổng số lượng truyện
        public int? CurrentPage { get; set; }  // Trang hiện tại
        public int? TotalPages { get; set; }   // Tổng số trang
    }
}
