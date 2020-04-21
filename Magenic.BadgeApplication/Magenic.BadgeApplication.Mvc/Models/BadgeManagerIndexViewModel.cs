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
        /// Gets or sets the corporate badge header
        /// </summary>
        public string CorporateBadgeHeader { get; set; }
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

        /// <summary>
        /// Gets or sets the show add button flag.
        /// </summary>
        public bool ShowAddButton { get; set; }

        /// <summary>
        /// Gets or sets the show community badges flag.
        /// </summary>
        public bool ShowCommunityBadges { get; set; }
    }
}