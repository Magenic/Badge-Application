using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountInfoIndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInfoIndexViewModel"/> class.
        /// </summary>
        /// <param name="earnedBadges">The earned badges.</param>
        public AccountInfoIndexViewModel(IEarnedBadgeCollection earnedBadges)
        {
            EarnedBadges = earnedBadges;
        }

        /// <summary>
        /// Gets or sets the account information.
        /// </summary>
        /// <value>
        /// The account information.
        /// </value>
        public IAccountInfoEdit AccountInfo { get; set; }

        /// <summary>
        /// Gets the earned badges.
        /// </summary>
        /// <value>
        /// The earned badges.
        /// </value>
        public IEarnedBadgeCollection EarnedBadges { get; private set; }
    }
}