using JXGIS.ReactFrame.BaseLib.Bundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace JXGIS.ReactFrame.Web
{
    public class BundleConfig
    {
        private static string _extendPath = "~/Reference/";
        public static void RegisterBundles(BundleCollection bundles)
        {
            //全局
            bundles.Add(new Bundle("~/gStyle").Include(_extendPath + "antd-2.2.0/antd.min.css"));
            bundles.Add(new Bundle("~/gScripts").Include(
                _extendPath + "react-15.3.2/react.min.js",
                _extendPath + "react-15.3.2/react-dom.min.js",
                _extendPath + "antd-2.2.0/antd.min.js"));





        }
    }
}