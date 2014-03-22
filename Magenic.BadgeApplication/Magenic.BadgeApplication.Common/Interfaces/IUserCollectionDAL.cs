
using System.Collections.Generic;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IUserCollection"/>.
    /// </summary>

    public interface IUserCollectionDAL
    {
        /// <summary>
        /// Returns all users that a user is listed as managing in AD.
        /// The user's own information will always be returned by this request.
        /// </summary>
        /// <param name="managerId">The system id of the user to find employees for.</param>
        /// <param name="isAdmin">A <see cref="bool"/> indicating if this is an administrative user.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;UserItemDTO&gt;" />.</returns>
        Task<IEnumerable<UserItemDTO>> GetUsersForMangerIdAsync(int managerId, bool isAdmin);


    }
}
