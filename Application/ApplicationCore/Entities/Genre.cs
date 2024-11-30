using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Entities
{
    public class Genre : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
