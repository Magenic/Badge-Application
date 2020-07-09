using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.DTO
{
    public class UserPermissionDTO
    {
        public int EmployeePermissionId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PermissionType PermissionId { get; set; }
    }
}
