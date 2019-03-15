using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IfiNavetNotifier.Extentions;
using IfiNavetNotifier.Logger;

[assembly: InternalsVisibleTo("IfiNavet.Infrastructure.Tests")]

namespace IfiNavetNotifier.Web
{
    public class EventWebClient : IEventClient
    {
        public EventWebClient(HttpCookieClient httpCookieClient, ILogger logger)
        {
            Logger = logger;   
            Scraper = new EventScraper(httpCookieClient, Logger);
        }

        public ILogger Logger { get; }
        private EventScraper Scraper { get; }


        public async Task<IEnumerable<IfiEvent>> GetEvents()
        {
            var result = await Scraper.GetAllEventLinks();
            if (result == null)
                return null;
            
            var events = new ConcurrentBag<IfiEvent>();

            await result.ForEachAsync(100, async uri =>  events.Add(await Scraper.GetEvent(uri)));

            return events;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            return await Scraper.GetEvent(uri);
        }

//        public bool LoggInn(UserLogin user)
//        {
//            return Parser.LoginUser(user);
//        }
    }
}