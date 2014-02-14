using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.WebApi.Controllers
{
    [Authorize]
    public class ActivityController : ApiController
    {
        // GET api/activity
        public IEnumerable<ExternalActivitySubmissionDTO> Get()
        {
            return new List<ExternalActivitySubmissionDTO>();
        }

        // GET api/activity/5
        public ExternalActivitySubmissionDTO Get(int id)
        {
            return new ExternalActivitySubmissionDTO();
        }

        // POST api/activity
        public async Task<IHttpActionResult> Post([FromBody]ExternalActivitySubmissionDTO activity)
        {
            //Get the employee ID
            ICustomIdentityDAL customIdentityDAL = IoC.Container.Resolve<ICustomIdentityDAL>();
            var identity = await customIdentityDAL.RetrieveIdentityAsync(activity.UserADName);

            var submitActivity = SubmitActivity.CreateActivitySubmission(identity.Id);
            submitActivity.Notes = activity.Notes;
            submitActivity.ActivityId = activity.ActivityId;

            submitActivity = (ISubmitActivity)submitActivity.Save();

            return Ok();
        }

        // PUT api/activity/5
        public void Put(int id, [FromBody]ExternalActivitySubmissionDTO value)
        {
        }

        // DELETE api/activity/5
        public void Delete(int id)
        {
        }
    }
}
