using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace IfiNavetNotifier
{
    public class IfiWebsiteParser
    {
        public HttpClient hc { get; set; }

        public IfiWebsiteParser()
        {

            hc = new HttpClient();

        }
        public IEnumerable<IfiEvent> GetEvents()
        {
            return GetEvents(GetEventIds());
        }

        public IEnumerable<IfiEvent> GetEvents(IEnumerable<String> eventIds)
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
                    /*Date = DateTime.Parse(htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[1]/p").InnerText),*/
                    Food = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[3]/p").InnerText,
                    PlacesLeft = int.Parse(htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[4]/p").InnerHtml.Split("</span>").FirstOrDefault(n => Regex.IsMatch(n, @"^\d")).Split('<').FirstOrDefault()),
                    Link = url
                };

                var datePatt = @"dd.MM.yyyy HH:mm";
                var date = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[3]/div/div[1]/div[2]/div[1]/p").InnerText;
                ievent.Date = DateTime.ParseExact(date, datePatt, null);



                events.Add(ievent);
            }
            return events;

        }




        public  IEnumerable<String> GetEventIds()
        {
            string urlAddress = "https://ifinavet.no/event";
            string data = string.Empty;
            data =  hc.GetStringAsync(urlAddress).Result;


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
