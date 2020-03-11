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

        public static string FloorNumber_AR(this int floorNumber)
        {
            switch (floorNumber)
            {
                case 1:
                    return "الدور الأول";
                case 2:
                    return "الدور الثاني";
                case 3:
                    return "الدور الثالث";
                case 4:
                    return "الدور الرابع";
                case 5:
                    return "الدور الخامس";
                default:
                    return "";
            }
        }

        public static string UserNumber_AR(this int userNumber)
        {
            switch (userNumber)
            {
                case 1:
                    return "المستخدم الأول";
                case 2:
                    return "المستخدم الثاني";
                case 3:
                    return "المستخدم الثالث";
                case 4:
                    return "المستخدم الرابع";
                case 5:
                    return "المستخدم الخامس";
                case 6:
                    return "المستخدم السادس";
                case 7:
                    return "المستخدم السابع";
                case 8:
                    return "المستخدم الثامن";
                case 9:
                    return "المستخدم التاسع";
                case 10:
                    return "المستخدم العاشر";
                default:
                    return "";
            }
        }
    }
}

