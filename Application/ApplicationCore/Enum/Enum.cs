using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enum
{
    public enum UserRole
    {
        [Description("Administrator")]
        Admin = 1,

        [Description("User")]
        User = 2,
    }

    public enum UserRank
    {
        [Description("Bronze")]
        Bronze = 1,

        [Description("Silver")]
        Silver = 2,

        [Description("Gold")]
        Gold = 3,
    }

}
