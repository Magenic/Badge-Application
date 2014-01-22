using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing ICslaIdentity.
    /// </summary>

    public interface ICustomIdentityDAL
    {
        /// <summary>
        /// Logs on a Custom Identity.
        /// </summary>
        /// <param name="userName">The user name to log in with.</param>
        /// <param name="password">The unencrypted password to use.</param>
        /// <returns>A <see cref="CustomIdentityDTO"/> with the information needed to 
        /// load the custom identity.</returns>
        Task<CustomIdentityDTO> LogOnIdentityAsync(string userName, string password);
    }
}
