﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Incentivapp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                            .Include("~/Content/bootstrap/css/bootstrap.min.css",
                                    "~/Content/styles.css"));
            bundles.Add(new ScriptBundle("~/bundles/js")
                        .Include("~/Content/js/jquery.min.js", "~/Content/bootstrap/js/bootstrap.min.js"));
        }
    }
}