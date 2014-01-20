using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="IBadgeEdit"/>.
    /// </summary>
    public interface IBadgeEditDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; set; }
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
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; set; }
        /// <summary>
        /// The date and time when the badge was created.
        /// </summary>
        DateTime Created { get; set; }
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
        int ApprovedById { get; set; }
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// A byte array with the image for the badge, used for saving only.
        /// </summary>
        byte[] BadgeImage { get; set; }
    }
}
