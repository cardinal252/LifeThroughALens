using System;
using Sitecore.Data;

namespace LifeThroughALens.Domain
{
    public class Theme : SitecoreItem, ITheme
    {
        public string CssFile { get; set; }

        public string GetCssFullPath()
        {
            return String.Format("/assets/{0}", CssFile);
        }
    }
}
