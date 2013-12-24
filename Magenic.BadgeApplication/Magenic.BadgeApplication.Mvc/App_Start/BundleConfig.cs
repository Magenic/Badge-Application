using Magenic.BadgeApplication.Common;
using System.Web.Optimization;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// 
    /// </summary>
    public static class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            Arg.IsNotNull(() => bundles);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.0.0.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/badgePage").Include(
                "~/Scripts/badgePage.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/activityPage").Include(
                "~/Scripts/activityPage.js"
            ));
        }
    }
}
