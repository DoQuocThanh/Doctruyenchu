using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class UserWithItemsSpecification : Specification<User>
    {
        public UserWithItemsSpecification(string email)
        {
            Query
                .Where(b => b.Email == email);
                
        }

        public UserWithItemsSpecification(int userId)
        {
            Query
                .Where(b => b.Id == userId).Include(b => b.Stories);

        }


    }
}
