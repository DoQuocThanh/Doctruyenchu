using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ChapterWithItemsSpecification : Specification<Chapter>
    {
        public ChapterWithItemsSpecification(int storyId)
        {
            Query
           .Where(c => c.StoryId == storyId)
           .OrderBy(c => c.CreatedAt);
        }



    }
}
