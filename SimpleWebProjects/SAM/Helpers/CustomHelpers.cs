using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAM.Models;
using System.Text;

namespace SAM.Helpers
{
    public static class CustomHelpers
    {
        public static IHtmlString Menu(this HtmlHelper helper, List<Menu> items)
        {
            return new MvcHtmlString(FormatItems(items));
        }

        private static string FormatItems(List<Menu> items)
        {
            StringBuilder res = new StringBuilder();
            if (items.Any())
            {
                res.Append("<ul>");

                foreach (var item in items)
                {
                    res.Append("<li id=\"" + item.Id + "\">");
                    res.Append(item.Name);
                    res.Append(FormatItems(item.ChildMenu));
                    res.Append("</li>");
                }
                res.Append("</ul>");
            }
            return res.ToString();
        }
    }
}