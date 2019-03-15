using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace IfiNavetNotifier.Extentions
{
    public static class StringExtentions
    {
        public static string ToTitleCase(this string s) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
        public static string RuleToRegular(this string org)
        {
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
            
            return builder.ToString().Replace(" Rule", "");
        }
    }
}