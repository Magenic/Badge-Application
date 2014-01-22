using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Data.Entity.Infrastructure;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class AccountInfoEditDAL : IAccountInfoEditDAL
    {
        public async Task<AccountInfoEditDTO> GetAccountInfoByEmployeeIdAsync(int employeeId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from e in ctx.Employees
                                         join b in ctx.BadgeAwards on e.EmployeeId equals b.EmployeeId
                                          where e.EmployeeId == employeeId
                                         group b by new { e.EmployeeId, e.ADName, e.AwardPayoutThreshold } into g
                                         select new AccountInfoEditDTO
                                         {
                                             EmployeeId = g.Key.EmployeeId,
                                             UserName = g.Key.ADName,
                                             PointPayoutThreshold = g.Key.AwardPayoutThreshold,
                                             TotalPointsEarned = g.Sum(t => t.AwardAmount),
                                             TotalPointsPaidOut = g.Any(t => t.PaidOut) ? g.Where(t => t.PaidOut).Sum(t => t.AwardAmount) : 0
                                         }).ToArrayAsync();

                var activity = activityList.Single();
                return activity;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2200:RethrowToPreserveStackDetails")]
        public AccountInfoEditDTO Update(AccountInfoEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                ctx.Configuration.ValidateOnSaveEnabled = false;
                var saveEmployee = LoadData(data);
                ctx.Employees.Attach(saveEmployee);
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveEmployee).SetModifiedProperty("AwardPayoutThreshold");
                
                ctx.SaveChanges();
            }
            return data;
        }

        private static Employee LoadData(AccountInfoEditDTO data)
        {
            var employeeEntity = new Employee
            {
                EmployeeId = data.EmployeeId,
                ADName = data.UserName,
                AwardPayoutThreshold = data.PointPayoutThreshold
            };
            return employeeEntity;
        }
    }
}
