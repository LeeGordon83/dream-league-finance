﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString EmbedCss(this HtmlHelper htmlHelper, string path)
        {
            var cssFilePath = HttpContext.Current.Server.MapPath(path);

            try
            {
                var cssText = File.ReadAllText(cssFilePath);
                return htmlHelper.Raw("<style>\n" + cssText + "\n</style>");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}