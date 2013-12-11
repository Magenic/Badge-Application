using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface to edit badges.
    /// </summary>
    public interface IBadgeEdit : Csla.IBusinessBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        string Tagline { get; set; }
        /// <summary>
        /// The long description of the badge.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        Enums.BadgeType Type { get; set; }

        /// <summary>
        /// Set's the image to be associated with the badge.
        /// </summary>
        void SetBadgeImage(byte[] image);
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; }
        /// <summary>
        /// The date and time when the badge was created.
        /// </summary>
        DateTime Created { get; }
        /// <summary>
        /// The date and time of when the badge is effective and can be awarded.
        /// </summary>
        DateTime? EffectiveStartDate { get; set; }
        /// <summary>
        /// the date and time of when the badge is no longer effective and cannot be awarded.
        /// </summary>
        DateTime? EffectiveEndDate { get; set; }
        /// <summary>
        /// A numeric priority for display order of badges.
        /// </summary>
        int Priority { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if this badge can be awarded multiple times.
        /// </summary>
        bool MultipleAwardsPossible { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if a badge that has been awarded multiple times
        /// should only display once.
        /// </summary>
        bool DisplayOnce { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if management approval is required before
        /// this badge is awarded.
        /// </summary>
        bool ManagementApprovalRequired { get; set; }
        /// <summary>
        /// Check with Steve.  
        /// </summary>
        int ActivityPointsAmount { get; set; }
        /// <summary>
        /// Check with Steve.
        /// </summary>
        int AwardValueAmount { get; set; }
        /// <summary>
        /// The id of the person who approved this badge so it can be awarded.
        /// </summary>
        int? ApprovedById { get; set; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        DateTime? ApprovedDate { get; }
    }
}
