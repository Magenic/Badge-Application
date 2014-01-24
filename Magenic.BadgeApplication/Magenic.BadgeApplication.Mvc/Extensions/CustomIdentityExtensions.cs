using Magenic.BadgeApplication.BusinessLogic.Security;
using Magenic.BadgeApplication.Common.Interfaces;
using Newtonsoft.Json;
using System.Security.Principal;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomIdentityExtensions
    {
        /// <summary> 
        /// Create serialized string for storing in a cookie 
        /// </summary> 
        /// <returns>String representation of identity</returns> 
        public static string ToJson(this IIdentity customIdentity)
        {
            return JsonConvert.SerializeObject(customIdentity);
        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="customIdentityJson">The custom identity json.</param>
        /// <returns></returns>
        public static ICustomIdentity FromJson(this string customIdentityJson)
        {
            return JsonConvert.DeserializeObject<CustomIdentity>(customIdentityJson);
        }
    }
}