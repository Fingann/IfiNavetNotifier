using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace IfiNavetNotifier.Web
{
    public class EventParser
    {
        public EventParser(CookieClient cookieClient, UserLogin user = null)
        {
            Client = cookieClient;
            BaseUri = new Uri("http://ifinavet.no/");
        }

        private Uri BaseUri { get; }
        private CookieClient Client { get; }

        public async Task<IEnumerable<Uri>> GetAllEventLinks()
        {
            var doc = new HtmlDocument();
            var uri = new Uri(BaseUri + "event");
            var html = await Client.GetStringAsync(uri);
            doc.LoadHtml(html);

            var linkedPages = doc.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => Regex.IsMatch(u, "/event/\\d")).Select(x => new Uri(BaseUri + x.Substring(1)));
            return linkedPages;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            var doc = new HtmlDocument();
            var html = await Client.GetStringAsync(uri);
            doc.LoadHtml(html);
            var parsedEvent = EventMapper.Map(uri, doc);
            return parsedEvent;
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