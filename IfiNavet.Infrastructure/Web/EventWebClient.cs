using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Users;

namespace IfiNavet.Infrastructure.Web
{
    using IfiNavet.Core.Interfaces.Web;
    public class EventWebClient :IEventClient
    {
        private CookieAwareWebClient Client { get; }
        public EventParser Parser { get;  } 
        public EventWebClient( UserLogin user = null)
        {
            Client = new CookieAwareWebClient();
            if (user != null)
            {
                Parser = new EventParser(user);
            }
            else
            {
                Parser = new EventParser();
            }
        }


        public async Task<IEnumerable<IfiEvent>> GetEvents()
        {
            var result = await Parser.GetEventLinks();
            List<IfiEvent> events = new List<IfiEvent>();
            Parallel.ForEach(await Parser.GetEventLinks(),
                async (Link) => { events.Add(await Parser.GetEvent(Link)); });
           
            return events;
        }

        
        public async Task<IfiEvent> GetEvent(Uri uri)
        {
            return await Parser.GetEvent(uri);
        }
    }
}