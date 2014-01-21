using Csla.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for dealing with the custom principal object.
    /// </summary>
    public interface ICustomPrincipal : ICslaPrincipal
    {
        /// <summary>
        /// Returns a <see cref="ICustomIdentity"/> for this principal. 
        /// </summary>
        /// <returns>The <see cref="ICustomIdentity"/> for this principal.  Returns 
        /// null if not castable to this type</returns>
        ICustomIdentity CustomIdentity();
    }
}
