using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Entities
{
    public class Story : IAggregateRoot
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int View { get; set; }
        public User User { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
