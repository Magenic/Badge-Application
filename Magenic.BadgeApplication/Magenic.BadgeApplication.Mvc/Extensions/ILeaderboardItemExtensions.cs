using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Globalization;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ILeaderboardItemExtensions
    {
        /// <summary>
        /// Percentages the completed.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <param name="totalBadgeCount">The total badge count.</param>
        /// <returns></returns>
        public static int PercentageCorporateCompleted(this ILeaderboardItem leaderboardItem, int totalBadgeCount)
        {
            Arg.IsNotNull(() => leaderboardItem);

            if (totalBadgeCount == 0)
            {
                totalBadgeCount = 1;
            }

            var percentageCompleted = ((double)leaderboardItem.EarnedCorporateBadgeCount / totalBadgeCount) * 100;
            return (int)Math.Round(percentageCompleted, 0);
        }

        /// <summary>
        /// Percentages the community completed string.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <param name="totalBadgeCount">The total badge count.</param>
        /// <returns></returns>
        public static string PercentageCorporateCompletedString(this ILeaderboardItem leaderboardItem, int totalBadgeCount)
        {
            Arg.IsNotNull(() => leaderboardItem);

            if (totalBadgeCount == 0)
            {
                totalBadgeCount = 1;
            }

            var percentageCompleted = ((double)leaderboardItem.EarnedCorporateBadgeCount / totalBadgeCount);
            return percentageCompleted.ToString("P0", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Percentages the completed.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <param name="totalBadgeCount">The total badge count.</param>
        /// <returns></returns>
        public static int PercentageCommunityCompleted(this ILeaderboardItem leaderboardItem, int totalBadgeCount)
        {
            Arg.IsNotNull(() => leaderboardItem);

            if (totalBadgeCount == 0)
            {
                totalBadgeCount = 1;
            }

            var percentageCompleted = ((double)leaderboardItem.EarnedCommunityBadgeCount / totalBadgeCount) * 100;
            return (int)Math.Round(percentageCompleted, 0);
        }

        /// <summary>
        /// Percentages the community completed string.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <param name="totalBadgeCount">The total badge count.</param>
        /// <returns></returns>
        public static string PercentageCommunityCompletedString(this ILeaderboardItem leaderboardItem, int totalBadgeCount)
        {
            Arg.IsNotNull(() => leaderboardItem);

            if (totalBadgeCount == 0)
            {
                totalBadgeCount = 1;
            }

            var percentageCompleted = ((double)leaderboardItem.EarnedCommunityBadgeCount / totalBadgeCount);
            return percentageCompleted.ToString("P0", CultureInfo.CurrentCulture);
        }
    }
}