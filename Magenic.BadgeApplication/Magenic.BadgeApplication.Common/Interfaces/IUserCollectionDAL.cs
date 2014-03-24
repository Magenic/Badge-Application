
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
        /// <param name="userId">The system id of the user to find employees for.</param>
        /// <param name="isManager">A <see cref="bool"/> indicating if this is a manager user.</param>
        /// <param name="isAdmin">A <see cref="bool"/> indicating if this is an administrative user.</param>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerable&lt;UserItemDTO&gt;" />.</returns>
        Task<IEnumerable<UserItemDTO>> GetUsersForIdAsync(int userId, bool isManager, bool isAdmin);

        /// <summary>
        /// Returns the AD name of all users in the system without a termination date.
        /// </summary>
        /// <returns>A list of AD user names.</returns>
        IEnumerable<string> GetActiveAdUsers();
    }
}
