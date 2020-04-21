using Magenic.BadgeApplication.Common.Enums;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class to pass inactive badge search parameters
    /// </summary>
    public sealed class InactiveBadgeSearchDTO
    {
        /// <summary>
        /// Gets or sets the badge type search parameter
        /// </summary>
        public BadgeType BadgeType { get; set; }

        /// <summary>
        /// Gets or sets the effective date search parameter
        /// </summary>
        public DateTime InactiveEffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the badge status list search parameter
        /// </summary>
        public IEnumerable<BadgeStatus> BadgeStatusList { get; set; }
    }
}
