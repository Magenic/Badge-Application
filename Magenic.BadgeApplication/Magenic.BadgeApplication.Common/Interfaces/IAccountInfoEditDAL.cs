using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IAccountInfoEdit"/>.
    /// </summary>
    public interface IAccountInfoEditDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="IAccountInfoEditDTO"/> for the specified employee id.
        /// </summary>
        /// <param name="employeeId">The employee id of the user to search for.</param>
        /// <returns>An <see cref="IAccountInfoEditDTO"/>.</returns>
        Task<IAccountInfoEditDTO> GetAccountInfoByEmployeeIdAsync(int employeeId);
        /// <summary>
        /// Updates an existing account info based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IAccountInfoEditDTO Update(IAccountInfoEditDTO data);
    }
}
