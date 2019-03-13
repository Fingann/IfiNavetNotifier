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
        public EventWebClient(CookieClient cookieClient)
        {
            Parser = new EventParser(cookieClient);
        }

        internal EventParser Parser { get; }


        public async Task<IEnumerable<IfiEvent>> GetEvents()
        {
            var result = await Parser.GetAllEventLinks();
            var events = new ConcurrentBag<IfiEvent>();

            await result.ForEachAsync(100, async uri => events.Add(await Parser.GetEvent(uri)));

            return events;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            return await Parser.GetEvent(uri);
        }

//        public bool LoggInn(UserLogin user)
//        {
//            return Parser.LoginUser(user);
//        }
    }
}