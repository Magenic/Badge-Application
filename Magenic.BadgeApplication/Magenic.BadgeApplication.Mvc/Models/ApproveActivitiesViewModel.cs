using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ApproveActivitiesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApproveActivitiesViewModel"/> class.
        /// </summary>
        /// <param name="activitiesToApprove">The activities to approve.</param>
        public ApproveActivitiesViewModel(IApproveActivityCollection activitiesToApprove)
        {
            ActivitiesToApprove = activitiesToApprove;
        }

        /// <summary>
        /// Gets or sets the admin activities.
        /// </summary>
        /// <value>
        /// The admin activities.
        /// </value>
        public IApproveActivityCollection ActivitiesToApprove { get; private set; }
    }
}