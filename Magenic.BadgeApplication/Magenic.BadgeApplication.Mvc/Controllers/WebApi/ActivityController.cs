using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using System.Web.Http;

namespace Magenic.BadgeApplication.Controllers.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]SubmitActivityDTO value)
        {
            Arg.IsNotNull(() => value);
        }
    }
}