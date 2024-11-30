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
        public int UserId { get; set; } // Liên kết tới User
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; } // Thể loại lưu dạng text id
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
