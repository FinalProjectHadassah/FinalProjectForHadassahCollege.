using System.Web.Optimization;

namespace NewForumProject
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                        "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap-rtl").Include(
                        "~/Content/bootstrap-rtl.min.css"));


            bundles.Add(new StyleBundle("~/css/beyond").Include(
                        "~/Content/beyond.min.css",
                        "~/Content/demo.min.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/typicons.min.css",
                        "~/Content/weather-icons.min.css",
                        "~/Content/animate.min.css"
                        ));

            bundles.Add(new StyleBundle("~/css/beyond-rtl").Include(
                        "~/Content/beyond-rtl.min.css",
                        "~/Content/demo.min.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/typicons.min.css",
                        "~/Content/weather-icons.min.css",
                        "~/Content/animate.min.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/skin").Include(
                        "~/Scripts/skins.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery.min.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/slimscroll/jquery.slimscroll.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/beyond").Include(
                         "~/Scripts/js/beyond.min.js"
                         ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/js/jqueryval/jquery.validate*"
            //            ));
        }
    }
}