using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeReadOnly
        : IBusinessBase
    {
        /// <summary>
        /// The id of the badge.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// The name of a badge.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        Enums.BadgeType Type { get; }
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        string ImagePath { get; }
    }
}
