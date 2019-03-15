using System;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier.Web.Mapper
{
    internal static class HtmlToEventMapper
    {
        private const string NAME_PATH = "/html/body/div[3]/div/div[1]/h1";
        private const string FOOD_PATH = "/html/body/div[3]/div/div[1]/div[2]/div[3]/p";
        private const string LOCATION_PATH = "/html/body/div[3]/div/div[1]/div[2]/div[2]/p";
        private const string PLACES_LEFT_PATH = "/html/body/div[3]/div/div[1]/div[2]/div[4]/p";
        private const string DATE_PATH = "/html/body/div[3]/div/div[1]/div[2]/div[1]/p";
        private const string BUTTON_PATH = @"//*[@id=""message-form""]/button";


        
        public static IfiEvent Map(Uri url, HtmlDocument doc)
        {
            
            var ifiEvent = new IfiEvent
            {
                Name = EventNameCleaner.CleanName(doc.GetInnerText(NAME_PATH)),
                Food = doc.GetInnerText(FOOD_PATH),
                Location = doc.GetInnerText(LOCATION_PATH).Trim(),
                URL = url.ToString(),
                PlacesLeft = GetPlacesLeft(doc),
                Date = doc.GetInnerText(DATE_PATH).toDate("dd.MM.yyyy HH:mm") ?? new DateTime()
            };
            
            ifiEvent.Open = IsOpen(doc, ifiEvent);
            return ifiEvent;
        }

        

        private static bool IsOpen(HtmlDocument doc, IfiEvent ifiEvent)
        {
            var buttonText = doc.GetInnerText(BUTTON_PATH);
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
                .SelectSingleNode(PLACES_LEFT_PATH).ChildNodes?
                .Where(n => n is HtmlTextNode && Regex.IsMatch(n.InnerText, @"\d"))
                .Aggregate(string.Empty, (a, b) => a + b.InnerText);

            return int.TryParse(placesLeftString, out var value) ? value : 0;
        }

        
    }
}