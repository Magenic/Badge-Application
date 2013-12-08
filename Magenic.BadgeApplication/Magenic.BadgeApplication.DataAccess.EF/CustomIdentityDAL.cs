using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class CustomIdentityDAL : ICustomIdentityDAL
    {
        public async Task<ICustomIdentityDTO> LogOnIdentityAsync(string userName, string password)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employeeList = await (from t in ctx.Employees
                    where t.ADName == userName
                    select new Common.DTO.CustomIdentityDTO
                    {
                        Id = t.EmployeeId,
                        Name = t.ADName
                    }).ToArrayAsync();

                var employee = employeeList.SingleOrDefault();

                if (employee != null)
                {
                    var roles = from r in ctx.EmployeePermissions
                        where r.EmployeeId == employee.Id
                        select new
                        {
                            r.PermissionId
                        };

                    foreach (var role in roles)
                    {
                        ((List<string>)employee.Roles).Add(((Common.Enums.BadgeType)role.PermissionId).ToString());
                    }
                }
                return employee;
            }

        }
    }
}
