using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeManagerIndexViewModel
    {
        /// <summary>
        /// Gets or sets the corporate badges.
        /// </summary>
        /// <value>
        /// The corporate badges.
        /// </value>
        public IEnumerable<IBadgeItem> CorporateBadges { get; set; }

        /// <summary>
        /// Gets or sets the community badges.
        /// </summary>
        /// <value>
        /// The community badges.
        /// </value>
        public IEnumerable<IBadgeItem> CommunityBadges { get; set; }
    }
}