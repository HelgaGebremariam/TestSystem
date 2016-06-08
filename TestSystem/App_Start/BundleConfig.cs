﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TestSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jsScripts").Include(
                        "~/Scripts/jquery-1.9.1.min.js",
                        "~/Scripts/jquery.jqplot.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",
                        "~/Scripts/excanvas.min.js",
                        "~/Scripts/jsgrid.min.js"
                        ));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive").Include(
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/Site.css",
                      "~/Scripts/jquery.jqplot.min.css",
                      "~/Content/jsgrid.min.css"
                      ));
        }
    }
}