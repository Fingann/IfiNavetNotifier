using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace IfiNavetNotifier.Extentions
{
    public static class StringExtentions
    {
        public static string ToTitleCase(this string s) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
        public static string ToRuleName(this string org)
        {
            //org = FiveLeftRule
            StringBuilder builder = new StringBuilder();
            builder.Append(org[0]);
            for (var i = 1; i < org.Length; i++)
            {
                var tempChar = org[i];
                if (Char.IsUpper(tempChar))
                {
                    builder.Append(" " + tempChar);
                }
                else
                {
                    builder.Append(tempChar);
                }
            }
            //builder = Five Left Rule
            return builder.ToString().Replace(" Rule", "");
            //Returns Five Left
        }
    }
}