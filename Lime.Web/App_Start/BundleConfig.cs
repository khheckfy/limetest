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
                       "~/Scripts/jquery-ui-{version}.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/bo").Include(
                       "~/Scripts/bo/App.js"
                       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/themes/base/all.css"
                     ));

            #endregion
        }
    }
}