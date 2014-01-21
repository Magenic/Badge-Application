using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    [Serializable]
    public class BadgeEditDTO : Interfaces.IBadgeEditDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        public string Tagline { get; set; }
        /// <summary>
        /// The long description of the badge.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        public Enums.BadgeType Type { get; set; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        public string ImagePath { get; set;  }
        /// <summary>
        /// The date and time when the badge was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// The date and time of when the badge is effective and can be awarded.
        /// </summary>
        public DateTime? EffectiveStartDate { get; set; }
        /// <summary>
        /// the date and time of when the badge is no longer effective and cannot be awarded.
        /// </summary>
        public DateTime? EffectiveEndDate { get; set; }
        /// <summary>
        /// A numeric priority for display order of badges.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if this badge can be awarded multiple times.
        /// </summary>
        public bool MultipleAwardsPossible { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if a badge that has been awarded multiple times
        /// should only display once.
        /// </summary>
        public bool DisplayOnce { get; set; }
        /// <summary>
        /// A <see cref="bool"/> indicating if management approval is required before
        /// this badge is awarded.
        /// </summary>
        public bool ManagementApprovalRequired { get; set; }
        /// <summary>
        /// Check with Steve.  
        /// </summary>
        public int ActivityPointsAmount { get; set; }
        /// <summary>
        /// Check with Steve.
        /// </summary>
        public int AwardValueAmount { get; set; }
        /// <summary>
        /// The id of the person who approved this badge so it can be awarded.
        /// </summary>
        public int ApprovedById { get; set; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// A byte array with the image for the badge, used for saving only.
        /// </summary>
        public byte[] BadgeImage { get; set; }
    }
}
