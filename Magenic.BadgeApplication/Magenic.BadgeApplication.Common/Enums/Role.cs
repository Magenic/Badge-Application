using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// Security Roles for use with the badge applicaiton.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Normal sysem user
        /// </summary>
        User = 0,
        /// <summary>
        /// User who has administrative access to do things such as approve badges.
        /// </summary>
        Administrator = 1
    }
}
