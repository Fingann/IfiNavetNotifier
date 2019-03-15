using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IfiNavetNotifier.Logger;
using IfiNavetNotifier.Web.Mapper;

namespace IfiNavetNotifier.Web
{
    public class EventScraper
    {
        public EventScraper(HttpCookieClient httpCookieClient, ILogger logger)
        {
            Logger = logger;
            Client = httpCookieClient;
            BaseUri = new Uri("http://ifinavet.no/");
        }

        private ILogger Logger { get; }
        private Uri BaseUri { get; }
        private HttpCookieClient Client { get; }

        public async Task<IEnumerable<Uri>> GetAllEventLinks()
        {
            var doc = await RetrieveHtmlDocument(new Uri(BaseUri + "event"));
           
           
            var linkedPages = doc.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => Regex.IsMatch(u, "/event/\\d")).Select(x => new Uri(BaseUri + x.Substring(1)));
            return linkedPages;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            var doc = await RetrieveHtmlDocument(uri);
            var parsedEvent = HtmlToEventMapper.Map(uri, doc);
            return parsedEvent;
        }

        private async Task<HtmlDocument> RetrieveHtmlDocument(Uri uri)
        {
            var doc = new HtmlDocument();
            try
            {
                var html = await Client.GetStringAsync(uri);
                doc.LoadHtml(html);
            }
            catch (HttpRequestException e)
            {
                Logger.Exception("HTTP exception ", e);
                return null;
            }
            return doc;
        }
    }
}