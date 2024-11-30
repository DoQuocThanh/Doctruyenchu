using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Entities
{
    public class Comment : IAggregateRoot
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Liên kết tới User
        public int StoryId { get; set; } // Liên kết tới Story
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Story Story { get; set; }
    }
}
