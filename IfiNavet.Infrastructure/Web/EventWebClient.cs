using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Infrastructure.Extentions;
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("IfiNavet.Infrastructure.Tests")]

namespace IfiNavet.Infrastructure.Web
{
    using IfiNavet.Core.Interfaces.Web;
    public class EventWebClient :IEventClient
    {
        private CookieAwareHttpClient Client { get; }
        internal EventParser Parser { get;  } 
        public EventWebClient( UserLogin user = null)
        {
            Client = new CookieAwareHttpClient();
            Parser = user != null ? new EventParser(user) : new EventParser();
        }


        public async Task<IEnumerable<IfiEvent>> GetEvents()
        {
            var result = await Parser.GetEventLinks();
            ConcurrentBag<IfiEvent> events = new ConcurrentBag<IfiEvent>();

            await result.ForEachAsync(100, (async uri => events.Add(await Parser.GetEvent(uri))));

            return events;
        }

        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            return await Parser.GetEvent(uri);
        }
        
        
    }
}