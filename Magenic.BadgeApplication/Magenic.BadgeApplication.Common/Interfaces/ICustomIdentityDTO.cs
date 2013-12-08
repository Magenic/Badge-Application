using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations with the
    /// ICSLACustomIdentity object.
    /// </summary>
    public interface ICustomIdentityDTO
    {
        /// <summary>
        /// The Id for this indentity.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The name for the identity.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// An enumerable of <see cref="string"/> for all the roles the user is in.
        /// </summary>
        IEnumerable<string> Roles { get; set; }
    }
}
