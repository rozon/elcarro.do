using System.Web.Optimization;

namespace ElCarro.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JS
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/materialize.js",
                "~/Scripts/init.js",
                "~/Scripts/SumitForms.js"));

            bundles.Add(new ScriptBundle("~/bundles/map-init").Include(
                "~/Scripts/Store/map-init.js"));

            bundles.Add(new ScriptBundle("~/bundles/map-search").Include(
                "~/Scripts/Home/Search/map-init.js"));

            bundles.Add(new ScriptBundle("~/bundles/common_functions").Include(
                "~/Scripts/common_functions.js"));
            #endregion

            #region CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/materialize.css",
                "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/map-store").Include(
                "~/Content/Store/map-store.css"));

            bundles.Add(new StyleBundle("~/Content/Home_Search").Include(
                "~/Content/Home/Search/map-search.css"));

            bundles.Add(new StyleBundle("~/Content/account").Include(
                "~/Content/materialize.css",
                "~/Content/account_style.css"));
            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}
