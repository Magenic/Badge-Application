using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Constants;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.WindowsAzure.Storage;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class CustomIdentityDAL : ICustomIdentityDAL
    {
        public async Task<CustomIdentityDTO> RetrieveIdentityAsync(string userName)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employeeList = await (from t in ctx.Employees
                                          where t.ADName == userName
                                          select new CustomIdentityDTO
                                          {
                                              Id = t.EmployeeId,
                                              ADName = t.ADName
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
                        ((List<string>)employee.Roles).Add(((Common.Enums.PermissionType)role.PermissionId).ToString());
                    }
                }
                return employee;
            }
        }

        public CustomIdentityDTO SaveIdentity(AuthorizeLogOnDTO customIdentity)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employee = ctx.Employees.SingleOrDefault(e => e.ADName == customIdentity.UserName);
                if (employee == null)
                {
                    employee = new Employee();
                    LoadEmployee(employee, customIdentity);
                    employee.AwardPayoutThreshold = EmployeeConstants.DefaultAwardPayoutThreshold;
                    ctx.Employees.Add(employee);
                }
                else
                {
                    LoadEmployee(employee, customIdentity);
                }
                ctx.SaveChanges();

                AttachAndSavePermission(ctx, employee.EmployeeId, Common.Enums.PermissionType.User);

                return new CustomIdentityDTO
                {
                    Id = employee.EmployeeId,
                    ADName = employee.ADName
                };
            }
        }

        private void AttachAndSavePermission(Entities ctx, int employeeId, Common.Enums.PermissionType permissionType)
        {
            if (!ctx.EmployeePermissions.Any(ep => ep.EmployeeId == employeeId &&
                ep.PermissionId == (int)permissionType))
            {
                var permission = new EmployeePermission
                {
                    EmployeeId = employeeId,
                    PermissionId = (int)permissionType
                };
                ctx.EmployeePermissions.Add(permission);
                ctx.SaveChanges();
            }
        }

        private void LoadEmployee(Employee employee, AuthorizeLogOnDTO customIdentity)
        {
            employee.ADName = customIdentity.UserName;
            employee.FirstName = customIdentity.FirstName;
            employee.LastName = customIdentity.LastName;
            employee.EmploymentStartDate = customIdentity.EmployementStartDate;
            employee.EmploymentEndDate = customIdentity.EmployementEndDate;
        }

        public void SaveEmployeePhoto(byte[] data, string fileName)
        {
            Arg.IsNotNull(() => data);

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageAccountConnectionString"]);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["StorageAccountBlobContainer"]);
            var blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.UploadFromByteArray(data, 0, data.Length, null, null, null);
        }

        public void SaveManagerInfo(AuthorizeLogOnDTO customIdentity)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employee = ctx.Employees.SingleOrDefault(e => e.ADName == customIdentity.UserName);
                if (employee != null)
                {
                    var manager1 = ctx.Employees.SingleOrDefault(e => e.ADName == customIdentity.Manager1ADName);
                    if (manager1 != null)
                    {
                        employee.ApprovingManagerId1 = manager1.EmployeeId;
                    }
                    var manager2 = ctx.Employees.SingleOrDefault(e => e.ADName == customIdentity.Manager2ADName);
                    if (manager2 != null)
                    {
                        employee.ApprovingManagerId2 = manager2.EmployeeId;
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void SetManagerPermission(string employeeADName)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employee = ctx.Employees.SingleOrDefault(e => e.ADName == employeeADName);
                if (employee != null)
                {
                    if (ctx.Employees.Any(e => e.ApprovingManagerId1 == employee.EmployeeId
                                               || e.ApprovingManagerId2 == employee.EmployeeId))
                    {
                        AttachAndSavePermission(ctx, employee.EmployeeId, Common.Enums.PermissionType.Manager);
                    }
                }
            }
        }

        public void SetTerminationDate(string employeeADName, System.DateTime? termDate)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var employee = ctx.Employees.SingleOrDefault(e => e.ADName == employeeADName);
                if (employee != null)
                {
                    employee.EmploymentEndDate = termDate;
                }
                ctx.SaveChanges();
            }
        }
    }
}