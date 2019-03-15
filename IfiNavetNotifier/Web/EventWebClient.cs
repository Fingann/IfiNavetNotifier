using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IfiNavetNotifier.Extentions;

[assembly: InternalsVisibleTo("IfiNavet.Infrastructure.Tests")]

namespace IfiNavetNotifier.Web
{
    public class EventWebClient : IEventClient
    {
        public EventWebClient(HttpCookieClient httpCookieClient)
        {
            Crawler = new EventCrawler(httpCookieClient);
        }

        private EventCrawler Crawler { get; }


        public async Task<IEnumerable<IfiEvent>> GetEvents()
        {
            var result = await Crawler.GetAllEventLinks();
            if (result == null)
                return null;
            
            var events = new ConcurrentBag<IfiEvent>();

            await result.ForEachAsync(100, async uri =>  events.Add(await Crawler.GetEvent(uri)));

            return events;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            return await Crawler.GetEvent(uri);
        }

//        public bool LoggInn(UserLogin user)
//        {
//            return Parser.LoginUser(user);
//        }
    }
}