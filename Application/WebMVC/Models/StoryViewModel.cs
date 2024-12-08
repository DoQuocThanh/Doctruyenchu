using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class StoryViewModel
    {
        public int StoryId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

    }
}
