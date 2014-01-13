using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeIndexViewModel
    {
        /// <summary>
        /// Gets or sets the corporate badges.
        /// </summary>
        /// <value>
        /// The corporate badges.
        /// </value>
        public IEnumerable<IBadgeItem> CorporateBadgesTopRow { get; set; }

        /// <summary>
        /// Gets or sets the corporate badges bottom row.
        /// </summary>
        /// <value>
        /// The corporate badges bottom row.
        /// </value>
        public IEnumerable<IBadgeItem> CorporateBadgesBottomRow { get; set; }

        /// <summary>
        /// Gets or sets the corporate earned badges.
        /// </summary>
        /// <value>
        /// The corporate earned badges.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CorporateEarnedBadgesTopRow { get; set; }

        /// <summary>
        /// Gets or sets the corporate earned badges bottom row.
        /// </summary>
        /// <value>
        /// The corporate earned badges bottom row.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CorporateEarnedBadgesBottomRow { get; set; }

        /// <summary>
        /// Gets or sets the community badges.
        /// </summary>
        /// <value>
        /// The community badges.
        /// </value>
        public IEnumerable<IBadgeItem> CommunityBadgesTopRow { get; set; }

        /// <summary>
        /// Gets or sets the community badges bottom row.
        /// </summary>
        /// <value>
        /// The community badges bottom row.
        /// </value>
        public IEnumerable<IBadgeItem> CommunityBadgesBottomRow { get; set; }

        /// <summary>
        /// Gets or sets the community earned badges.
        /// </summary>
        /// <value>
        /// The community earned badges.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CommunityEarnedBadgesTopRow { get; set; }

        /// <summary>
        /// Gets or sets the community earned badges.
        /// </summary>
        /// <value>
        /// The community earned badges.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CommunityEarnedBadgesBottomRow { get; set; }

        /// <summary>
        /// Gets or sets the possible activities.
        /// </summary>
        /// <value>
        /// The possible activities.
        /// </value>
        public IEnumerable<SelectListItem> PossibleActivities { get; set; }

        /// <summary>
        /// Gets or sets the new activity.
        /// </summary>
        /// <value>
        /// The new activity.
        /// </value>
        public SubmitActivityViewModel NewlySubmittedActivity { get; set; }
    }
}