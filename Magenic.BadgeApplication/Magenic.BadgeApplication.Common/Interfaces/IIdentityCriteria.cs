using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Specifies informaiton to use to fetch an Identity.
    /// </summary>
    public interface IIdentityCriteria
    {
        /// <summary>
        /// The username for the identity.
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The unencrypted password.
        /// </summary>
        string Password { get; set; }
    }
}
