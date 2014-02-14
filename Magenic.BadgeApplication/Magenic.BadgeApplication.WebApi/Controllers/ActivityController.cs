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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
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
    }
}
