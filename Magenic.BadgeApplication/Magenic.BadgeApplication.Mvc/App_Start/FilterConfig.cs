using Magenic.BadgeApplication.Common;
using System.Web.Mvc;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// 
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Arg.IsNotNull(() => filters);

            filters.Add(new HandleErrorAttribute());
        }
    }
}
