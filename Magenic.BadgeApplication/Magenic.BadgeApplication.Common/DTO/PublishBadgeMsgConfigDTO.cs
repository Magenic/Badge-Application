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
    public class PublishBadgeMsgConfigDTO
    {
        /// <summary>
        /// The Environemnt to be published
        /// </summary>
        public string Environment { get; set; }
        /// <summary>
        /// The title of the message to be published
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Id of the employee
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// The full name of the employee
        /// </summary>
        public string EmployeeFullName { get; set; }
        /// <summary>
        /// The first name of the employee
        /// </summary>
        public string EmployeeFirstName { get; set; }
        /// <summary>
        /// The last name of the employee
        /// </summary>
        public string EmployeeLastName { get; set; }
        /// <summary>
        /// The last name of the employee
        /// </summary>
        public string EmployeeEmailAddress { get; set; }
        /// <summary>
        /// The Active Directory Name of the employee which may include the Doman Name
        /// </summary>
        public string EmployeeADName { get; set; }
        /// <summary>
        /// The Active Directory Name of the employee without Doman Name
        /// </summary>
        public string EmployeeADNameNoDomain { get; set; }
        /// <summary>
        /// The URL of the Leaderboard
        /// </summary>
        public string Leaderboard { get; set; }
        /// <summary>
        /// The URL of the Leaderboard for the employee
        /// </summary>
        public string EmployeeLeaderboard { get; set; }
        /// <summary>
        /// The URL of the Magenic DataService
        /// </summary>
        public string MagenicDataService { get; set; }
        /// <summary>
        /// The list of the QueueItemIds
        /// </summary>
        public IList<PublishQueueItemDTO> QueueItems { get; set; }
    }
}
