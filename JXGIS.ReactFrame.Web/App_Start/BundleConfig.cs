using JXGIS.Common.BaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JXGIS.ReactFrame.Web
{
    public class BundleConfig
    {
        private static string _refPath = "~/Reference/";

        public static void RegisterBundles(BundleCollection bundles)
        {
            //全局style
            bundles.Add(new Bundle("~/gStyle").Include(
                _refPath + "antd-2.2.0/antd.min.css"));
            //全局script
            bundles.Add(new Bundle("~/gScripts").Include(
                _refPath + "react-15.3.2/react.min.js",
                _refPath + "react-15.3.2/react-dom.min.js",
                _refPath + "antd-2.2.0/antd.min.js"));

            bundles.Add(new LessBundle("~/Test/index/css").Include("~/Views/Test/css/index.less"));
            bundles.Add(new BabelBundle("~/Test/index/js").Include("~/Views/Test/js/index.jsx"));
        }
    }
}