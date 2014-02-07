using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Magenic.BadgeApplication.WebApi.Controllers
{
    public class ActivityController : ApiController
    {
        // GET api/activity
        public IEnumerable<ActivityEditDTO> Get()
        {
            return new List<ActivityEditDTO>();
        }

        // GET api/activity/5
        public ActivityEditDTO Get(int id)
        {
            return new ActivityEditDTO();
        }

        // POST api/activity
        public void Post([FromBody]ActivityEditDTO activity)
        {
            var activityEdit = ActivityEdit.CreateActivity();
            activityEdit.Name = activity.Name;
            activityEdit.Description = activity.Description;
            
            activityEdit = (IActivityEdit)activityEdit.Save();
        }

        // PUT api/activity/5
        public void Put(int id, [FromBody]ActivityEditDTO value)
        {
        }

        // DELETE api/activity/5
        public void Delete(int id)
        {
        }
    }
}
