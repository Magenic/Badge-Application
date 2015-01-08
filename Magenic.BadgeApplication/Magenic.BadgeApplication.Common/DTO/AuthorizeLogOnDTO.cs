using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer authorization operations.
    /// </summary>
    public class AuthorizeLogOnDTO
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
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The user's location.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The user's department.
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// AD name of the user's primary manager.
        /// </summary>
        public string Manager1ADName { get; set; }
        /// <summary>
        /// AD name of the user's secondary manager.
        /// </summary>
        public string Manager2ADName { get; set; }
        /// <summary>
        /// The employee's start date.
        /// </summary>
        public DateTime? EmployementStartDate { get; set; }
        /// <summary>
        /// The employee's end date.
        /// </summary>
        public DateTime? EmployementEndDate { get; set; }
    }
}
