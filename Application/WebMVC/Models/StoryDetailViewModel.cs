using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class StoryDetailViewModel
    {
        public int StoryId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int View { get; set; }
        public ICollection<GenreViewModel> Genres { get; set; }
        public ICollection<ChapterViewModel>? Chapters { get; set; }
        public ICollection<ChapterViewModel>? ChaptersCloumOne { get; set; }
        public ICollection<ChapterViewModel>? ChaptersCloumTwo { get; set; }

        public int CurrentPage { get; set; }  // Trang hiện tại
        public int TotalPages { get; set; }   // Tổng số trang

    }
}
