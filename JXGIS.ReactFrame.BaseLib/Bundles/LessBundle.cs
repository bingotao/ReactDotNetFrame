using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace JXGIS.ReactFrame.BaseLib.Bundles
{
    public class LessBundle : Bundle
    {
        public LessBundle(string virtualPath) : base(virtualPath, new IBundleTransform[] { new LessTransform(), new CssMinify() })
        {

        }

        public LessBundle(string virtualPath, string cdnPath) : base(virtualPath, cdnPath, new IBundleTransform[] { new LessTransform(), new CssMinify() })
        {

        }
    }
}
