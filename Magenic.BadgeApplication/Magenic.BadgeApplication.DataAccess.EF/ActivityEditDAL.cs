using Magenic.BadgeApplication.Common.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ActivityEditDAL : IActivityEditDAL
    {
        public async Task<IActivityEditDTO> GetActivityByIdAsync(int activityEditId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from t in ctx.Activities
                                          where t.ActivityId == activityEditId
                    select new Common.DTO.ActivityEditDTO
                    {
                        Id = t.ActivityId,
                        Name = t.ActivityName,
                        Description = t.ActivityDescription,
                        RequiresApproval = t.RequiresApproval
                    }).ToArrayAsync();

                var activity = activityList.SingleOrDefault();

                return activity;
            }
        }

        public IActivityEditDTO Update(IActivityEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveActivity = LoadData(data);
                ctx.Activities.Attach(saveActivity);
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveActivity).SetModifiedProperty("ActivityName");
                objectState.GetObjectStateEntry(saveActivity).SetModifiedProperty("ActivityDescription");
                objectState.GetObjectStateEntry(saveActivity).SetModifiedProperty("RequiresApproval");

                ctx.SaveChanges();
                data.Id = saveActivity.ActivityId;
            }
            return data;
        }

        private static Activity LoadData(IActivityEditDTO data)
        {
            var activityEntity = new Activity
            {
                ActivityId = data.Id,
                ActivityName = data.Name,
                ActivityDescription = data.Description,
                RequiresApproval = data.RequiresApproval
            };
            return activityEntity;
        }

        public IActivityEditDTO Insert(IActivityEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveActivity = LoadData(data);
                ctx.Activities.Add(saveActivity);

                ctx.SaveChanges();
                data.Id = saveActivity.ActivityId;
            }
            return data;
        }

        public void Delete(int activityId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var deleteActivity = new Activity
                {
                    ActivityId = activityId
                };
                ctx.Activities.Attach(deleteActivity);
                ctx.Activities.Remove(deleteActivity);
                ctx.SaveChanges();
            }
        }
    }
}
