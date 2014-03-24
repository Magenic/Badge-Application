
namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class UserItemDTO
    {
        /// <summary>
        /// The employee Id of the person.
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// The user's first name as reported by AD.
        /// </summary>
        public string EmployeeFirstName { get; set; }
        /// <summary>
        /// The user's last name.
        /// </summary>
        public string EmployeeLastName { get; set; }
    }
}