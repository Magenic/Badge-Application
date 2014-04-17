using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Configuration;
using System.Globalization;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ILeaderboardItemExtensions
    {
        /// <summary>
        /// Mediums the profile photo location.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <returns></returns>
        public static string MediumProfilePhotoLocation(this ILeaderboardItem leaderboardItem)
        {
            Arg.IsNotNull(() => leaderboardItem);

            var mediumPhotoLocationFormat = ConfigurationManager.AppSettings["MediumProfilePhoto"];
            return String.Format(CultureInfo.CurrentCulture, mediumPhotoLocationFormat, leaderboardItem.EmployeeADName);
        }

        /// <summary>
        /// Larges the profile photo location.
        /// </summary>
        /// <param name="leaderboardItem">The leaderboard item.</param>
        /// <returns></returns>
        public static string LargeProfilePhotoLocation(this ILeaderboardItem leaderboardItem)
        {
            Arg.IsNotNull(() => leaderboardItem);

            var mediumPhotoLocationFormat = ConfigurationManager.AppSettings["LargeProfilePhoto"];
            return String.Format(CultureInfo.CurrentCulture, mediumPhotoLocationFormat, leaderboardItem.EmployeeADName);
        }
    }
}