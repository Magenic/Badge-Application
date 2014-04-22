using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardCompareViewModel
    {
        /// <summary>
        /// Gets or sets the left leaderboard item.
        /// </summary>
        public ILeaderboardItem LeftLeaderboardItem { get; set; }

        /// <summary>
        /// Gets or sets the right leaderboard item.
        /// </summary>
        public ILeaderboardItem RightLeaderboardItem { get; set; }

        /// <summary>
        /// Gets or sets all badges.
        /// </summary>
        public IBadgeCollection AllBadges { get; set; }

        /// <summary>
        /// Gets or sets all activities.
        /// </summary>
        public IActivityCollection AllActivities { get; set; }
    }
}