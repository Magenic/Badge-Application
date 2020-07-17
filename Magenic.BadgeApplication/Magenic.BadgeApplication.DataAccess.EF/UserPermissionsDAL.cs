using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class UserPermissionsDAL : IUserPermissionsDAL
    {
        public async Task<IEnumerable<UserPermissionDTO>> GetAll()
        {
            using (var ctx = new Entities())
            {
                var items = await (ctx.Employees
                    .Include("EmployeePermissions")
                    .Include("Permission")
                    .Where(item => item.EmployeePermissions.Any())
                    .Select(item => new UserPermissionDTO 
                    { 
                        EmployeePermissionId = item.EmployeePermissions.FirstOrDefault().EmployeePermissionId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PermissionId = (PermissionType)item.EmployeePermissions.FirstOrDefault().PermissionId,
                    })).ToArrayAsync();

                return items;
            }
        }

        public async Task<UserPermissionDTO> GetById(int employeePermissionId)
        {
            using (var ctx = new Entities())
            {
                var userPermissionDTO = await (ctx.EmployeePermissions
                    .Include("Employees")
                    .Include("Permission")
                    .Where(item => item.EmployeePermissionId == employeePermissionId)
                    .Select(item => new UserPermissionDTO
                    {
                        EmployeePermissionId = employeePermissionId,
                        FirstName = item.Employee.FirstName,
                        LastName = item.Employee.LastName,
                        PermissionId = (PermissionType)item.PermissionId,
                    })).FirstOrDefaultAsync();

                return userPermissionDTO;
            }
        }

        public void Update(UserPermissionDTO userPermissionsDTO)
        {
            using (var ctx = new Entities())
            {
                var userPermission = ctx.EmployeePermissions.FirstOrDefault(item => item.EmployeePermissionId == userPermissionsDTO.EmployeePermissionId);

                userPermission.PermissionId = (int)userPermissionsDTO.PermissionId;

                ctx.SaveChanges();
            }
        }
    }
}
