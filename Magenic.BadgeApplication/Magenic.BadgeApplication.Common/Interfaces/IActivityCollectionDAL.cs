using System.Collections.Generic;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IActivityCollection"/>.
    /// </summary>
    public interface IActivityCollectionDAL
    {
        /// <summary>
        /// Returns an <see cref="System.Collections.IEnumerable"/> for all activities in the system.
        /// </summary>
        /// <param name="managerActivities">Return activities that can only be entered by managers.</param>
        /// <param name="adminActivities">Return activities that can only be entered by people in the administrator role.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IEnumerable<ActivityItemDTO>> GetAllActvitiesAsync(bool managerActivities, bool adminActivities);
    }
}
