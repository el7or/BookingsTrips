using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BookingsTrips.Helper
{
    public static class Extensions
    {
        public static string Normalize_AR(this string text)
        {
            IDictionary<string, string> normalizeMap = new Dictionary<string, string>()
            {
                {"أ","ا"},
                {"إ","ا"},
                {"آ","ا"},
                {"ة","ه"},
                {"ى","ي"},
                {"ئ","ي"},
                {"ؤ","و"}
            };

            return new Regex(String.Join("|", normalizeMap.Keys.Select(k => Regex.Escape(k))))
                .Replace(text, m => normalizeMap[m.Value]);

            // // إزالة علامات التشكيل
            //string normalizedText = "";
            //foreach (Char c in text)
            //{
            //    var clearChar = ((int)c).ToString("x").ToLower();//    
            //    if (clearChar != "64b" && clearChar != "64c" && clearChar != "64d" && clearChar != "64e" && clearChar != "64f" && clearChar != "650" && clearChar != "651" && clearChar != "652")
            //    { normalizedText += c.ToString(); }
            //}
            //return normalizedText;
        }
    }
}

