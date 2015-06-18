using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveActivitiesForManagerViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApproveActivitiesViewModel"/> class.
        /// </summary>
        /// <param name="activitiesToApprove">The activities to approve.</param>
        public ApproveActivitiesForManagerViewModel(IApproveActivityManagerCollection activitiesToApprove)
        {
            ActivitiesToApprove = activitiesToApprove;
        }

        /// <summary>
        /// Gets or sets the admin activities.
        /// </summary>
        /// <value>
        /// The admin activities.
        /// </value>
        public IApproveActivityManagerCollection ActivitiesToApprove { get; private set; }
    }
}