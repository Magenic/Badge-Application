using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public sealed class ActivityEditDTO
    {
        /// <summary>
        /// The id of the activity.  Zero if new.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the activity used to identify it.
        /// Multiple activities are not allowed to use the same name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Description of the activity.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Does an activity submission for this activity require managerial approval
        /// to be counted towards badges and awards?
        /// </summary>
        public bool RequiresApproval { get; set; }
        /// <summary>
        /// The id of the employee that created the activity.
        /// </summary>
        public int CreateEmployeeId { get; set; }
    }
}
