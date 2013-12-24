using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    [Serializable]
    public class BadgeItemDTO : IBadgeItemDTO
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        public Enums.BadgeType Type { get; set; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        public string ImagePath { get; set; }
    }
}