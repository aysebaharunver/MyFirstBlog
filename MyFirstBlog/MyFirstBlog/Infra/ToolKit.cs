using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstBlog.Infra
{
    public static class ToolKit
    {
        public static string SafeSubstring(string text, int start, int length)
        {
            // text paramettresinden gelen metin start parametresinden itibaren length paramtresi kadar substring ile alın<cak. Ancak text.length değeri parametrede ki length den kısa ise metnin tamamı döndürülecek

            int resultLength = text.Length < length ? text.Length : length;

            return text.Substring(start, resultLength);

        }


        public static string CleanHtml(string htmlText)
        {
            return htmlText.Replace("<h3>", "").Replace("</h3>", "")
                .Replace("<h1>", "").Replace("</h1>", "");
        }
    }
}