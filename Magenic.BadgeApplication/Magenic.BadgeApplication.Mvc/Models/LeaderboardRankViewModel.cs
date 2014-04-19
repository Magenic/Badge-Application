using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardRankViewModel
    {
        /// <summary>
        /// Gets or sets the top ten corporate badges.
        /// </summary>
        public IEnumerable<ILeaderboardItem> CorporateBadgeHolders { get; set; }

        /// <summary>
        /// Gets or sets the top ten community badges.
        /// </summary>
        public IEnumerable<ILeaderboardItem> CommunityBadgeHolders { get; set; }

        /// <summary>
        /// Gets or sets the total corporate badge count.
        /// </summary>
        public int TotalCorporateBadgeCount { get; set; }

        /// <summary>
        /// Gets or sets the total community badge count.
        /// </summary>
        public int TotalCommunityBadgeCount { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        [Display(ResourceType = typeof(ApplicationResources), Name = "SearchTextLabel")]
        public string SearchText { get; set; }
    }
}