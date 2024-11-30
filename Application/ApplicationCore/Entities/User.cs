using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Entities
{
    public class User : IAggregateRoot
    {
        public int Id { get; set; } // Sử dụng int cho ID
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Story> Stories { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
