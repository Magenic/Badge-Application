using Csla;
using Magenic.BadgeApplication.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeReadOnlyDAL
        : IBusinessBase
    {
        /// <summary>
        /// Asychronously returns an <see cref="IActivityEditDTO" /> for the specified id.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// An <see cref="IBadgeEditDTO" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<IBadgeReadOnlyDTO>> GetBadgesBadgeTypeAsync(BadgeType badgeType);
    }
}
