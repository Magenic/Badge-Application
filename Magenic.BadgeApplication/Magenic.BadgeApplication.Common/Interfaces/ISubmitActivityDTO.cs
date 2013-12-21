using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface for defining a data transfer object for persist operations 
    /// for classes implementing <see cref="ISubmitActivity"/>.
    /// </summary>
    public interface ISubmitActivityDTO
    {
        /// <summary>
        /// The Id for this activity submission.  Zero if new.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The id of the activity this submission is for.
        /// </summary>
        int ActivityId { get; set; }
        /// <summary>
        /// The date the activity occurred, should be set and saved in UTC.
        /// </summary>
        DateTime ActivitySubmissionDate { get; set; }
        /// <summary>
        /// Any notes associated with this submission.
        /// </summary>
        string Notes { get; set; }
        /// <summary>
        /// The AD user name of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The current status of this activity submission.
        /// </summary>
        Enums.ActivitySubmissionStatus Status { get; set; }
        /// <summary>
        /// The AD user name of the user who approved this activity.  Blank if the 
        /// activity status is approved and no managerial approval is required.
        /// </summary>
        string ApprovedByUserName { get; set;  }
    }
}
