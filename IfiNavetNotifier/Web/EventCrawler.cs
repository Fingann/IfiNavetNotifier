using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IfiNavetNotifier.Web.Mapper;

namespace IfiNavetNotifier.Web
{
    public class EventCrawler
    {
        public EventCrawler(HttpCookieClient httpCookieClient)
        {
            Client = httpCookieClient;
            BaseUri = new Uri("http://ifinavet.no/");
        }

        private Uri BaseUri { get; }
        private HttpCookieClient Client { get; }

        public async Task<IEnumerable<Uri>> GetAllEventLinks()
        {
            var doc = await RetriveHtmlDocument(new Uri(BaseUri + "event"));
           
           
            var linkedPages = doc.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => Regex.IsMatch(u, "/event/\\d")).Select(x => new Uri(BaseUri + x.Substring(1)));
            return linkedPages;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            var doc = await RetriveHtmlDocument(uri);
            var parsedEvent = HtmlToEventMapper.Map(uri, doc);
            return parsedEvent;
        }

        private async Task<HtmlDocument> RetriveHtmlDocument(Uri uri)
        {
           
            var doc = new HtmlDocument();
            try
            {
                var html = await Client.GetStringAsync(uri);
                doc.LoadHtml(html);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(DateTime.Now + " - " + e);
                return null;
            }
            return doc;
        }

//        public bool LoginUser(UserLogin user)
//        {
//            var loginCredentials = new List<KeyValuePair<string, string>>()
//            {
//                new KeyValuePair<string, string>("username", user.Username),
//                new KeyValuePair<string, string>("password", user.Password),
//                
//            };
//            var url = new Uri(BaseUri, "login");
//            var response = Client.PostAsync(url, new FormUrlEncodedContent(loginCredentials)).Result;
//            var temp = response.Content.ReadAsStringAsync().Result;
//
//            if (temp.Contains("Logg ut"))
//            {
//                return false;
//            }
//
//            return true;
//            //TODO: Logic for checking if login was success
//
//        }
    }
}