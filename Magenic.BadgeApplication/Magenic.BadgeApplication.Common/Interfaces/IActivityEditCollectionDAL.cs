using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IActivityCollection"/>.
    /// </summary>
    public interface IActivityEditCollectionDAL
    {
        /// <summary>
        /// Returns an <see cref="System.Collections.IEnumerable"/> for all activities in the system.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IEnumerable<ActivityEditDTO>> GetAllActvitiesAsync();
    }
}
