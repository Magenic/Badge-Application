using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Data Transfer Object for the CustomIdentity
    /// </summary>
    public sealed class SubmitBadgeRequestDTO
    {
        /// <summary>
        /// The id of the badge request this submission is for.
        /// </summary>
        public int BadgeRequestId { get; set; }
        /// <summary>
        /// The id of the person who this badge submission is for.  
        /// This should be the same as the id of the identity.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Any description associated with this submission.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Any description associated with this submission.
        /// </summary>
        public string Description { get; set; }
    }
}
