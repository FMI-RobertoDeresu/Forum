using System.Web.Optimization;

namespace Forum.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var styleBundle = new StyleBundle("~/css")
                .Include("~/Content/stylesheets/site.css", new CssRewriteUrlTransform())
                .Include("~/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/bower_components/bootstrap/dist/jquery.autogrow.min.css", new CssRewriteUrlTransform())
                .Include("~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform());
            styleBundle.Transforms.Add(new CssMinify());

            var scriptBundle = new ScriptBundle("~/js")
                .Include("~/bower_components/jquery/dist/jquery.min.js")
                .Include("~/bower_components/bootstrap/dist/js/bootstrap.min.js")
                .Include("~/bower_components/jquery-autogrow-textarea/dist/jquery.autogrow.min.js")
                .Include("~/Scripts/site.js");
            scriptBundle.Transforms.Add(new JsMinify());

            bundles.Add(styleBundle);
            bundles.Add(scriptBundle);
        }
    }
}