using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to approve badges.  Badges that are not approved cannot be
    /// awarded.
    /// </summary>
    public interface IApproveBadgeItem : Csla.IBusinessBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int BadgeId { get; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        string Tagline { get; }
        /// <summary>
        /// The long description of the badge.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        Enums.BadgeType Type { get; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; }
        /// <summary>
        /// Check with Steve.
        /// </summary>
        int AwardValueAmount { get; }
        /// <summary>
        /// The date and time when the badge was created.
        /// </summary>
        DateTime Created { get; }
        /// <summary>
        /// The id of the person who approved this badge so it can be awarded.
        /// </summary>
        int ApprovedById { get; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        DateTime? ApprovedDate { get; }
        /// <summary>
        /// The status of the badge.
        /// </summary>
        Enums.BadgeStatus BadgeStatus { get; }
        /// <summary>
        /// Approves a badge awaiting approval
        /// </summary>
        /// <param name="approverUserId">The id of the user who is approving the badge.</param>
        void ApproveBadge(int approverUserId);
        /// <summary>
        /// Denies a badge awaiting approval
        /// </summary>
        void DenyBadge();
    }
}
