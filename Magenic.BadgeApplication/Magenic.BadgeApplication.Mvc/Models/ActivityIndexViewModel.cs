using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityIndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityIndexViewModel"/> class.
        /// </summary>
        public ActivityIndexViewModel()
        {
            NewActivity = ActivityEdit.CreateActivity();
        }

        /// <summary>
        /// Gets or sets the new activity.
        /// </summary>
        /// <value>
        /// The new activity.
        /// </value>
        public IActivityEdit NewActivity { get; set; }
    }
}