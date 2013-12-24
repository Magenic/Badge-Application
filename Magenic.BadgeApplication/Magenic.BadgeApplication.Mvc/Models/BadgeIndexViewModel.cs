using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeIndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeIndexViewModel"/> class.
        /// </summary>
        public BadgeIndexViewModel()
        {
            CorporateBadgesTopRow = new List<IBadgeItem>();
            CorporateBadgesBottomRow = new List<IBadgeItem>();
            CorporateEarnedBadgesTopRow = new List<IBadgeItem>();
            CorporateEarnedBadgesBottomRow = new List<IBadgeItem>();
            CommunityBadgesTopRow = new List<IBadgeItem>();
            CommunityBadgesBottomRow = new List<IBadgeItem>();
            CommunityEarnedBadges = new List<IBadgeItem>();
            NewActivity = ActivityEdit.CreateActivity();
        }

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
        public IEnumerable<IBadgeItem> CorporateEarnedBadgesTopRow { get; set; }

        /// <summary>
        /// Gets or sets the corporate earned badges bottom row.
        /// </summary>
        /// <value>
        /// The corporate earned badges bottom row.
        /// </value>
        public IEnumerable<IBadgeItem> CorporateEarnedBadgesBottomRow { get; set; }

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
        public IEnumerable<IBadgeItem> CommunityEarnedBadges { get; set; }

        /// <summary>
        /// Gets or sets the new activity.
        /// </summary>
        /// <value>
        /// The new activity.
        /// </value>
        public IActivityEdit NewActivity { get; set; }
    }
}