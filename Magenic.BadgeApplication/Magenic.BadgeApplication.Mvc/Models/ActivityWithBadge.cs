using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityWithBadge
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>
        /// The activity.
        /// </value>
        public ISubmittedActivityItem SubmittedActivity { get; set; }

        /// <summary>
        /// Gets or sets the badge to display.
        /// </summary>
        /// <value>
        /// The badge to display.
        /// </value>
        public IBadgeItem BadgeToDisplay { get; set; }
    }
}