
namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface used by the CanCreate rule to retrieve the employee id
    /// of the employee that created the item.
    /// </summary>
    public interface ICreateEmployee
    {
        /// <summary>
        /// The employee id of the employee who created the item.
        /// </summary>
        int CreateEmployeeId { get; }
    }
}
