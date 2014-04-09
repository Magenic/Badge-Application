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
        public IEnumerable<IBadgeItem> CorporateBadges { get; set; }

        /// <summary>
        /// Gets or sets the corporate earned badges.
        /// </summary>
        /// <value>
        /// The corporate earned badges.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CorporateEarnedBadges { get; set; }

        /// <summary>
        /// Gets or sets the community badges.
        /// </summary>
        /// <value>
        /// The community badges.
        /// </value>
        public IEnumerable<IBadgeItem> CommunityBadges { get; set; }

        /// <summary>
        /// Gets or sets the community earned badges.
        /// </summary>
        /// <value>
        /// The community earned badges.
        /// </value>
        public IEnumerable<IEarnedBadgeItem> CommunityEarnedBadges { get; set; }

        /// <summary>
        /// Gets or sets the possible activities.
        /// </summary>
        /// <value>
        /// The possible activities.
        /// </value>
        public IEnumerable<SelectListItem> PossibleActivities { get; set; }

        /// <summary>
        /// Gets or sets the earned badge ListView model.
        /// </summary>
        /// <value>
        /// The earned badge ListView model.
        /// </value>
        public EarnedBadgeListViewModel EarnedBadgeListViewModel { get; set; }

        /// <summary>
        /// Gets or sets all activities.
        /// </summary>
        /// <value>
        /// All activities.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IActivityCollection AllActivities { get; set; }

        /// <summary>
        /// Gets or sets the new activity.
        /// </summary>
        /// <value>
        /// The new activity.
        /// </value>
        public ISubmitActivity SubmittedActivity { get; set; }

        /// <summary>
        /// A collection of user information that the current user is allowed to enter activities for.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IUserCollection AvailableUsers { get; set; }
    }
}