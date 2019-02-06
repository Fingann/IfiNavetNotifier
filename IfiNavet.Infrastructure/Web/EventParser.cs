using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Core.Interfaces.Web;

namespace IfiNavet.Infrastructure.Web
{
    public class EventParser : IWebParser<IfiEvent>
    {
        private Uri BaseUri { get;  }
        private CookieAwareWebClient Client { get; }
        private CookieAwareHttpClient Hclient { get; }
        public EventParser(UserLogin user = null)
        {
            Client = new CookieAwareWebClient();
            Hclient = new CookieAwareHttpClient();
            if (user != null)
            {
                
                Client.Login("https://ifinavet.no/login", user);
                //new
                Hclient,
            }
            BaseUri = new Uri("http://ifinavet.no/");
            
            
        }

        public async Task<IEnumerable<Uri>> GetEventLinks()
        {
            HtmlDocument doc = new HtmlDocument();
            var uri = new Uri(BaseUri + "event");    
            var html = await Client.DownloadStringTaskAsync(uri);
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
             
            var html = await Client.DownloadStringTaskAsync(uri);
            doc.LoadHtml(html);

            //var doc = await web.LoadFromWebAsync(uri.ToString());
            //var doc = web.Load(await Client.DownloadStringTaskAsync(uri));
            return EventMapper.Map(uri, doc);
        }

    }
}
