using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ActivityEditCollectionDAL : IActivityEditCollectionDAL
    {
        public async Task<IEnumerable<ActivityEditDTO>> GetAllActvitiesAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from t in ctx.Activities
                                          select new ActivityEditDTO
                    {
                        Id = t.ActivityId,
                        Name = t.ActivityName,
                        Description = t.ActivityDescription,
                        RequiresApproval = t.RequiresApproval,
                        CreateEmployeeId = t.CreateEmployeeId,
                        EntryType = (Common.Enums.ActivityEntryType)t.EntryTypeId
                    }).ToArrayAsync();

                return activityList;
            }
        }
    }
}
