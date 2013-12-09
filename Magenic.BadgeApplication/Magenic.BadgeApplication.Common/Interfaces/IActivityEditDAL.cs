using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IActivityEdit"/>.
    /// </summary>
    public interface IActivityEditDAL
    {
        /// <summary>
        /// Asychronously returns an <see cref="IActivityEditDTO"/> for the specified id.
        /// </summary>
        /// <param name="activityEditId">The actvity id to search for.</param>
        /// <returns>An <see cref="IActivityEditDTO"/>.</returns>
        Task<IActivityEditDTO> GetActivityByIdAsync(int activityEditId);
        /// <summary>
        /// Asynchronously updates an existing activity based on informaiton passed in via the DTO.
        /// </summary>
        /// <param name="data">the values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        Task<IActivityEditDTO> UpdateAsync(IActivityEditDTO data);
    }
}
