using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing ICslaIdentity.
    /// </summary>

    public interface ICustomIdentityDAL
    {
        /// <summary>
        /// retrieves information needed to load a Custom Identity.  Returns
        /// null if not found.
        /// </summary>
        /// <param name="userName">The user name to retrieve.</param>
        /// <returns>A <see cref="CustomIdentityDTO"/> with the information needed to 
        /// load the custom identity.</returns>
        Task<CustomIdentityDTO> RetrieveIdentityAsync(string userName);

        /// <summary>
        /// Saves information about the current identity.  If the identity is not found
        /// it will add it to the database.
        /// </summary>
        /// <param name="customIdentity">Information about the identity to insert.</param>
        /// <returns>A <see cref="CustomIdentityDTO"/> with information about the user that
        /// was saved or inserted.</returns>
        CustomIdentityDTO SaveIdentity(AuthorizeLogOnDTO customIdentity);
        /// <summary>
        /// Saves manager information for a given employee.
        /// </summary>
        /// <param name="customIdentity">Information about an employee, including their manager information.</param>
        void SaveManagerInfo(AuthorizeLogOnDTO customIdentity);
        /// <summary>
        /// Checks to see if any other employees list the given employee as a manager.  If so
        /// the manager permission is added.
        /// </summary>
        /// <param name="employeeADName">The active directory name of the person to check
        /// to see if he is a manager.</param>
        void SetManagerPermission(string employeeADName);
        /// <summary>
        /// Set the termination date on the record with the give AD name.
        /// </summary>
        /// <param name="employeeADName">The ad name to set the termination date for.</param>
        /// <param name="termDate">The termination date, send as null to clear the termination date.</param>
        void SetTerminationDate(string employeeADName, DateTime? termDate);

        /// <summary>
        /// Saves the employee photo.
        /// </summary>
        /// <param name="employeePhotoBytes">The employee photo bytes.</param>
        /// <param name="fileName">Name of the file.</param>
        void SaveEmployeePhoto(byte[] employeePhotoBytes, string fileName);
    }
}