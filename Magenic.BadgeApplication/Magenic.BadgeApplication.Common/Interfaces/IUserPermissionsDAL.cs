using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    public interface IUserPermissionsDAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<UserPermissionDTO>> GetAll();

        Task<UserPermissionDTO> GetById(int employeePermissionId);

        void Update(UserPermissionDTO userPermissionsDTO);
    }
}
