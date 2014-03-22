using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ActivityCollectionDAL : IActivityCollectionDAL
    {
        public async Task<IEnumerable<ActivityItemDTO>> GetAllActvitiesAsync(bool managerActivities, bool adminActivities)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from t in ctx.Activities
                                          where (t.EntryTypeId == (int)Common.Enums.ActivityEntryType.Any
                                            || (t.EntryTypeId == (int)Common.Enums.ActivityEntryType.Manager && (managerActivities || adminActivities))
                                            || (t.EntryTypeId == (int)Common.Enums.ActivityEntryType.Administrator && adminActivities))
                                          select new ActivityItemDTO
                                          {
                                              Id = t.ActivityId,
                                              Name = t.ActivityName,
                                              BadgeIds = t.BadgeActivities.Select(ba => ba.BadgeId),
                                          }).ToArrayAsync();

                return activityList;
            }
        }
    }
}
