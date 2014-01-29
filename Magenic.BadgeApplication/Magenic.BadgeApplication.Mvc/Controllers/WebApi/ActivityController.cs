using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        }       
    }
}