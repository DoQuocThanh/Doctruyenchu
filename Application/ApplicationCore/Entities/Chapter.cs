using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Entities
{
    public class Chapter : IAggregateRoot
    {
        public int Id { get; set; }
        public int StoryId { get; set; } // Liên kết tới Story
        public string Title { get; set; }
        public string Content { get; set; }
        //public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Story Story { get; set; }
    }
}
