using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ActivityCollectionDAL : IActivityCollectionDAL
    {
        public async Task<IEnumerable<IActivityItemDTO>> GetAllActvitiesAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from t in ctx.Activities
                    select new Common.DTO.ActivityItemDTO
                    {
                        Id = t.ActivityId,
                        Name = t.ActivityName
                    }).ToArrayAsync();

                return activityList;
            }
        }
    }
}
