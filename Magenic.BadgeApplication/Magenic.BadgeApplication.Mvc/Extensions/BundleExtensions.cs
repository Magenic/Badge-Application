using Magenic.BadgeApplication.Common;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class BundleExtensions
    {
        /// <summary>
        /// Includes the t4 MVC.
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <param name="virtualPaths">The virtual paths.</param>
        /// <returns></returns>
        public static Bundle IncludeT4MVC(this Bundle bundle, params string[] virtualPaths)
        {
            Arg.IsNotNull(() => bundle);

            bundle.Include(virtualPaths.Select(path => VirtualPathUtility.ToAppRelative(path)).ToArray());
            return bundle;
        }
    }
}