
namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents an activity submitted from an external source.
    /// </summary>
    public class ExternalActivitySubmissionDTO
    {
        /// <summary>
        /// The ID of the activity in the system
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// The Active Directory User Name of the user
        /// </summary>
        public string UserADName {get; set;}        

        /// <summary>
        /// A description of the activity submission
        /// </summary>
        public string Notes { get; set; }
    }
}
