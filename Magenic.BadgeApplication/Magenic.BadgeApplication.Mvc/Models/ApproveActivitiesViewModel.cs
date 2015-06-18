using Magenic.BadgeApplication.BusinessLogic.Activity;
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
        /// Initializes a new instance of the <see cref="ApproveActivitiesViewModel"/> class.
        /// </summary>
        /// <param name="activitiesToApprove">The activities to approve.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public ApproveActivitiesViewModel(IApproveActivityManagerCollection activitiesToApprove)
        {
            this.ActivitiesToApprove = new ApproveActivityCollection();
            foreach (ApproveActivityItemForManager t in activitiesToApprove)
            {
                ActivitiesToApprove.Add( new ApproveActivityItem(t));
            }
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