using System;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.Web.Mapper
{
    internal static class HtmlToEventMapper
    {
        public static IfiEvent Map(Uri url, HtmlDocument doc)
        {
            var ifiEvent = new IfiEvent
            {
                Name = EventNameCleaner.CleanName(doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/h1").InnerText),
                Food = doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[3]/p").InnerText,
                Location = doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[2]/p").InnerText
                    .Trim(),
                Link = url.ToString(),
                PlacesLeft = GetPlacesLeft(doc),
                Date = GetDate(doc)
            };
            
            ifiEvent.Open = IsOpen(doc, ifiEvent);
            return ifiEvent;
        }

        private static bool IsOpen(HtmlDocument doc, IfiEvent ifiEvent)
        {
            var buttonText = doc.DocumentNode.SelectSingleNode(@"//*[@id=""message-form""]/button").InnerText;
            switch (buttonText)
            {
                case "Logg inn":
                    if (ifiEvent.Date > DateTime.Now) return true;
                    return false;
                case "Meld deg på":
                    return true;
                case "Du er påmeldt, meld deg av":
                    return true;
                case "Påmelding er stengt":
                    return false;
                case "Arrangementet er ikke åpnet for påmelding":
                    return false;
                case "Arrangementet er fullt":
                    return true;
                default:
                    return false;
            }
        }

        private static int GetPlacesLeft(HtmlDocument htmlDoc)
        {
            var placesLeftString = htmlDoc.DocumentNode
                .SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[4]/p").ChildNodes?
                .Where(n => n is HtmlTextNode && Regex.IsMatch(n.InnerText, @"\d"))
                .Aggregate(string.Empty, (a, b) => a + b.InnerText);

            return int.TryParse(placesLeftString, out var value) ? value : 0;
        }

        private static DateTime GetDate(HtmlDocument htmlDoc)
        {
            var date = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[1]/p").InnerText;
            var dt = date.toDate("dd.MM.yyyy HH:mm");
            return dt ?? new DateTime();
        }
    }
}