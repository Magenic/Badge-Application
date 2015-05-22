using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Extensions;
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

            bundles.Add(new StyleBundle("~/Content/themes/base/css").IncludeT4MVC(
                Links.Content.themes.@base.all_css
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").IncludeT4MVC(
                Links.Scripts.jquery_2_1_4_min_js,
                Links.Scripts.jquery_unobtrusive_ajax_js,
                Links.Scripts.jquery_ui_1_11_4_min_js,
                Links.Scripts.tag_it_min_js,
                Links.Scripts.MicrosoftAjax_debug_js,
                Links.Scripts.MicrosoftMvcAjax_debug_js,
                Links.Scripts.MicrosoftMvcValidation_debug_js));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").IncludeT4MVC(
                Links.Scripts.modernizr_2_6_2_js));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").IncludeT4MVC(
                Links.Scripts.bootstrap_js,
                Links.Scripts.bootstrap_datepicker_js,
                Links.Scripts.bootstrap_filestyle_js,
                Links.Scripts.bootstrap_slider_js,
                Links.Scripts.bootstrap_select_js,
                Links.Scripts.respond_js,
                Links.Scripts.global_js));

            bundles.Add(new StyleBundle("~/Content/css").IncludeT4MVC(
                Links.Content.metro_bootstrap_css,
                Links.Content.font_awesome_css,
                Links.Content.bootstrap_datepicker_css,
                Links.Content.bootstrap_select_css,
                Links.Content.slider_css,
                Links.Content.jquery_tagit_css));

            bundles.Add(new StyleBundle("~/Content/siteCss").IncludeT4MVC(
                Links.Content.Site_css));

            bundles.Add(new ScriptBundle("~/bundles/jtable").IncludeT4MVC(
                Links.Scripts.jtable.jquery_jtable_js,
                Links.Scripts.activitiesManagement_js
            ));

            bundles.Add(new StyleBundle("~/Scripts/jtable/themes/metro/green/css").IncludeT4MVC(
                Links.Scripts.jtable.themes.metro.green.jtable_css
            ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").IncludeT4MVC(
                Links.Scripts.knockout_3_0_0_debug_js
            ));

            bundles.Add(new ScriptBundle("~/bundles/badgePage").IncludeT4MVC(
                Links.Scripts.badgePage_js
            ));

            bundles.Add(new ScriptBundle("~/bundles/activityPage").IncludeT4MVC(
                Links.Scripts.activityPage_js
            ));

            bundles.Add(new ScriptBundle("~/bundles/accountPage").IncludeT4MVC(
                Links.Scripts.accountPage_js
            ));

            bundles.Add(new ScriptBundle("~/bundles/badgeManager").IncludeT4MVC(
                Links.Scripts.badgeEditorPages_js,
                Links.Scripts.approveActivities_js,
                Links.Scripts.approveBadges_js
            ));
        }
    }
}
