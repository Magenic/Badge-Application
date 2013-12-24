using Magenic.BadgeApplication.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SubmitActivityViewModel
    {
        /// <summary>
        /// Gets or sets the new activity.
        /// </summary>
        /// <value>
        /// The new activity.
        /// </value>
        public SubmitActivityViewModel NewlySubmittedActivity { get; set; }
        /// <summary>
        /// The id of the activity this submission is for.
        /// </summary>
        [Display(Name = "ActivityTypeLabel", ResourceType = typeof(ApplicationResources))]
        public int ActivityId { get; set; }
        /// <summary>
        /// The date the activity occurred, should be set and saved in UTC.
        /// </summary>
        [Display(Name = "ActivitySubmissionDateLabel", ResourceType = typeof(ApplicationResources))]
        public DateTime ActivitySubmissionDate { get; set; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}