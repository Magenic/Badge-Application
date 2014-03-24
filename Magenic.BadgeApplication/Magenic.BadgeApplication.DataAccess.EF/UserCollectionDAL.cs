using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class UserCollectionDAL : IUserCollectionDAL
    {
        public async Task<IEnumerable<UserItemDTO>> GetUsersForIdAsync(int userId, bool isManager, bool isAdmin)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var userList = await(from t in ctx.Employees
                                         where (t.EmployeeId == userId
                                           || (isManager && t.ApprovingManagerId1 == userId || t.ApprovingManagerId2 == userId)
                                           || (isAdmin))
                                           && !t.EmploymentEndDate.HasValue
                                         orderby t.LastName, t.FirstName
                                         select new UserItemDTO
                                         {
                                             EmployeeId = t.EmployeeId,
                                             EmployeeFirstName = t.FirstName,
                                             EmployeeLastName = t.LastName
                                         }).ToArrayAsync();
                return userList;
            }
        }

        public IEnumerable<string> GetActiveAdUsers()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var userList = (from t in ctx.Employees
                                  where !t.EmploymentEndDate.HasValue
                                         select t.ADName);
                return userList;
            }
        }
    }
}
