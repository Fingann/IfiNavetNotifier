using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;


namespace IfiNavetNotifier.Web
{
    public class EventParser 
    {
        private Uri BaseUri { get;  }
        private CookieAwareHttpClient Client { get; }
        private bool LoggedInn { get; set; }
        public EventParser(UserLogin user = null)
        {
            Client = new CookieAwareHttpClient();
            BaseUri = new Uri("http://ifinavet.no/");
            LoggedInn = false;
        }

        public bool LoginUser(UserLogin user)
        {
            var loginCredentials = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", user.Username),
                new KeyValuePair<string, string>("password", user.Password),
                
            };
            var url = new Uri(BaseUri, "login");
            var response = Client.PostAsync(url, new FormUrlEncodedContent(loginCredentials));
           
            if (response.Contains("Logg ut"))
            {
                return true;
            }

            return false;
            //TODO: Logic for checking if login was success

        }

        public async Task<IEnumerable<Uri>> GetEventLinks()
        {
            HtmlDocument doc = new HtmlDocument();
            var uri = new Uri(BaseUri + "event");    
            var html = await Client.GetAsync(uri);
            doc.LoadHtml(html);
            
            //var doc = await web.LoadFromWebAsync(BaseUri + "event");
            var linkTags = doc.DocumentNode.Descendants("link");
            var linkedPages = doc.DocumentNode.Descendants("a")
                                              .Select(a => a.GetAttributeValue("href", null))
                                              .Where(u => Regex.IsMatch(u,"/event/\\d")).Select(x => new Uri(BaseUri +x.Substring(1)));


            return linkedPages;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            
            HtmlDocument doc = new HtmlDocument();
             
            var html = await Client.GetAsync(uri);
            doc.LoadHtml(html);

            //var doc = await web.LoadFromWebAsync(uri.ToString());
            //var doc = web.Load(await Client.DownloadStringTaskAsync(uri));
            var parsedEvent = EventMapper.Map(uri, doc);
            return parsedEvent;
        }

    }
}
