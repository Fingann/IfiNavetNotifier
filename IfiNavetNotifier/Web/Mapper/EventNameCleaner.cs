using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IfiNavetNotifier.Web.Mapper
{
    public static class EventNameCleaner
    {
        public static string CleanName(string name)
        {
            return Regex.Replace(name, "\\b" + string.Join("\\b|\\b", StringsToRemove) + "\\b", "");
        }
        private static IEnumerable<string> StringsToRemove { get; } =
            new List<string>
            {
                "Bedriftspresentasjon med ",
                "Bedriftspresentasjon ",
                "Bedriftspresentasjon hos ",
                "Kurskveld hos "
                
            };
    }
}