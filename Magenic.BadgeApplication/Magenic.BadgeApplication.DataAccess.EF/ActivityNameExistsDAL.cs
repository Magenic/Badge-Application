using Magenic.BadgeApplication.Common.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ActivityNameExistsDAL : IActivityNameExistsDAL
    {
        public async Task<bool> ActivityNameExistsAsync(string name)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityExists = (await (from a in ctx.Activities 
                     where a.ActivityName == name select new {a.ActivityName})
                     .ToListAsync()).Any();

                return activityExists;
            }
        }
    }
}
