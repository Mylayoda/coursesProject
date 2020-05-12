using System.Web;
using System.Web.Optimization;

namespace courses
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Admin").Include(
                     "~/Scripts/AdminMenu.js"));
            bundles.Add(new StyleBundle("~/Content/courses").Include(
                      "~/Content/navbar.css",
                      "~/Content/thumbnails.css",
                      "~/Content/CourseContent.css",
                      "~/Content/aboutus.css",
                      "~/Content/aboutus.css"));

        }
    }
}
