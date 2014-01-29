using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EarnedBadgeListViewModel
    {
        /// <summary>
        /// Gets or sets the badge items.
        /// </summary>
        /// <value>
        /// The badge items.
        /// </value>
        public IEnumerable<IBadgeItem> BadgeItems { get; set; }

        /// <summary>
        /// Gets or sets the badge activities.
        /// </summary>
        /// <value>
        /// The badge activities.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> EarnedBadges { get; set; }
    }
}