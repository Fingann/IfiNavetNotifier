using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IfiNavetNotifier.Extentions;

namespace IfiNavetNotifier
{
    public class IfiWebsiteParser : IWebParser<IfiEvent>
    {
        public HttpClient hc { get; set; }

        public IfiWebsiteParser()
        {

            hc = new HttpClient();

        }
        public IEnumerable<IfiEvent> GetEntitys()
        {
            try
            {
                return GetEntitys(GetEventIds());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<IfiEvent> GetEntitys(IEnumerable<String> eventIds)
        {
            List<IfiEvent> events = new List<IfiEvent>();

            foreach (var eventid in eventIds)
            {
                string url = $"https://ifinavet.no/{eventid}";
                string data = string.Empty;
                data = hc.GetStringAsync(url).Result;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(data);

                IfiEvent ievent = new IfiEvent
                {
                    Name = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/h1").InnerText,
                    Food = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[3]/p").InnerText,
                    Link = url
                };

                //Places left
                var temp = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[4]/p").InnerHtml.Split("</span>");
                var temp2 = String.Concat(temp.Where(n => Regex.IsMatch(n, @"^\d")).Select(x => x[0]));
                var temp3 = int.TryParse(temp2,out var placesLeft);
                ievent.PlacesLeft = placesLeft;

                //Date
                var date = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[1]/p").InnerText;
               
                var dt = date.toDate("dd.MM.yyyy HH:mm");
                if (dt.HasValue)
                {
                    ievent.Date = dt.Value;

                }

                events.Add(ievent);
            }
            return events;
        }

        public  IEnumerable<String> GetEventIds()
        {
            string urlAddress = "https://ifinavet.no/event";
            string data = string.Empty;
            try
            {
                data =  hc.GetStringAsync(urlAddress).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<string>();
            }
            
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);

            List<string> eventIDs = new List<string>();

            Regex reg = new Regex("/event/[0-9]");
            HtmlNode nodes =
            htmlDoc.DocumentNode.Descendants(0)
                .FirstOrDefault(n => n.HasClass("semester"));
            var links = nodes.SelectNodes("//a[@href]");

            foreach (HtmlNode link in links)
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);

                if (reg.IsMatch(hrefValue))
                {
                    eventIDs.Add(hrefValue);

                }

            }
            return eventIDs;
        }

       
    }
}
