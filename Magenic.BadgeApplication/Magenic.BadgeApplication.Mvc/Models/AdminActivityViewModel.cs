using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminActivityViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the submitted activity item.
        /// </summary>
        /// <value>
        /// The submitted activity item.
        /// </value>
        public ISubmittedActivityItem SubmittedActivityItem { get; set; }

        /// <summary>
        /// Gets or sets the approve activity button.
        /// </summary>
        /// <value>
        /// The approve activity button.
        /// </value>
        public string ApproveActivityButton { get; set; }

        /// <summary>
        /// Gets or sets the reject activity button.
        /// </summary>
        /// <value>
        /// The reject activity button.
        /// </value>
        public string RejectActivityButton { get; set; }
    }
}