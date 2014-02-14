using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveCommunityBadgesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApproveCommunityBadgesViewModel"/> class.
        /// </summary>
        /// <param name="approveActivityCollection">The approve activity collection.</param>
        public ApproveCommunityBadgesViewModel(IApproveBadgeCollection approveActivityCollection)
        {
            BadgesToApprove = approveActivityCollection;
        }

        /// <summary>
        /// Gets the activities to approve.
        /// </summary>
        /// <value>
        /// The activities to approve.
        /// </value>
        public IApproveBadgeCollection BadgesToApprove { get; private set; }
    }
}