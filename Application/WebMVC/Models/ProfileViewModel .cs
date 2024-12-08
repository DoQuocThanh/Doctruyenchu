using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class ProfileViewModel
    {
      
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? NumberStoriesWritten { get; set; }

        public ICollection<StoryViewModel> Stories { get; set; }

    }
}
