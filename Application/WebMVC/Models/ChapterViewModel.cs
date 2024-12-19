namespace WebMVC.Models
{
    public class ChapterViewModel
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public int Order { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? PreviousChapterId { get; set; } // ID của chương trước
        public int? NextChapterId { get; set; }     // ID của chương sau
    }
}
