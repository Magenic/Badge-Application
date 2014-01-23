using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    [Serializable]
    public class BadgeActivityEditDTO
    {
        /// <summary>
        /// The id for this relationship between a badge and an activity.
        /// </summary>
        public int BadgeActivityId { get; set; }
        /// <summary>
        /// The id of the prerequisite activity.
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// Indicates that this item should be removed from the persistence store.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
