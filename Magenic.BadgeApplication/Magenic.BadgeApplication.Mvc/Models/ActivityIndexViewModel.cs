using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityIndexViewModel
    {
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

        /// <summary>
        /// Gets or sets the previous activities.
        /// </summary>
        /// <value>
        /// The previous activities.
        /// </value>
        public IEnumerable<ActivityWithBadge> PreviousActivities { get; set; }
    }
}