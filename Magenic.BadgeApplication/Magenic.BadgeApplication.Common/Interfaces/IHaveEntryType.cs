using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Used by the CanCreate rule to get an activities entry type
    /// </summary>
    public interface IHaveEntryType
    {
        /// <summary>
        /// The current status of the badge
        /// </summary>
        ActivityEntryType EntryType { get; set; }
    }
}
