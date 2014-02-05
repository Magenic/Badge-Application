using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer authorization operations.
    /// </summary>
    public class AuthorizeLoginDTO
    {
        /// <summary>
        /// The AD user name of the user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The user's first name as reported by AD
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The user's last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Information about the user's primary manager.
        /// </summary>
        public AuthorizeLoginDTO Manager1 { get; set; }
        /// <summary>
        /// Information about the user's secondary manager.
        /// </summary>
        public AuthorizeLoginDTO Manager2 { get; set; }
        /// <summary>
        /// The employee's start date.
        /// </summary>
        public DateTime EmployementStartDate { get; set; }
        /// <summary>
        /// The employee's end date.
        /// </summary>
        public DateTime EmployementEndDate { get; set; }
    }
}
