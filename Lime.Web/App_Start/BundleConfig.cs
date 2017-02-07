using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Lime.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region base

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/nprogress.js",
                       "~/Scripts/jquery-ui-{version}.js",
                       "~/Scripts/bootstrap-notify.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/bo").Include(
                       "~/Scripts/bo/App.js",
                       "~/Scripts/bo/App.SalesReport.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/nprogress.css",
                     "~/Content/themes/base/all.css"
                     ));

            #endregion
        }
    }
}