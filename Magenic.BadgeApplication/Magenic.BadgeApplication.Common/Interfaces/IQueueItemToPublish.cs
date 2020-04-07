using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read only queue item to be published in a collection of badges.
    /// </summary>
    public interface IQueueItemToPublish : Csla.IBusinessBase
    {
        /// <summary>
        /// The ID of the QueueItem
        /// </summary>
        int QueueItemId { get; }

        /// <summary>
        /// The ID of the awarded badge
        /// </summary>
        int BadgeAwardId { get; }

        /// <summary>
        /// The date the queue item was created
        /// </summary>
        DateTime QueueItemCreated { get; }

        /// <summary>
        /// The ID of the Badge
        /// </summary>
        int BadgeId { get; }

        /// <summary>
        /// The Name of the Badge
        /// </summary>
        string BadgeName { get; }

        /// <summary>
        /// The TagLine of the Badge
        /// </summary>
        string BadgeTagline { get; }

        /// <summary>
        /// The Path of the Badge image
        /// </summary>
        string BadgePath { get; }

        /// <summary>
        /// The Description of the Badge
        /// </summary>
        string BadgeDescription { get; }

        /// <summary>
        /// The ID of the Enployee
        /// </summary>
        int EmployeeId { get; }

        /// <summary>
        /// The FirstName of the Employee
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The LastName of the Employee
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// The EmailAddress of the Employee
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// The ADName of the Employee
        /// </summary>
        string ADName { get; }
    }
}
