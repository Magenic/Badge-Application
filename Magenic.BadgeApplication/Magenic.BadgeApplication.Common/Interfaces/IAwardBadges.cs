using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Determines what badges will be awarded for an activity.
    /// </summary>
    public interface IAwardBadges
    {
        /// <summary>
        /// Returns a list of badges that would be created for a created activity.
        /// </summary>
        /// <param name="activityInfo">Information on the activity to create badges for.</param>
        /// <returns>A list of badges that would be created.</returns>
        IQueryable<BadgeAwardDTO> CreateBadges(ActivityInfoDTO activityInfo);
    }
}
