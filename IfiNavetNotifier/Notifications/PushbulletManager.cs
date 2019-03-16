using System;
using System.Collections.Generic;
using System.Linq;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;

namespace IfiNavetNotifier.Notifications
{
    public class PushbulletManager : INotifyManager
    {
        public PushbulletClient Client { get; set; }
        public string ApiKey { get; set; }
        
        public PushbulletManager()
        {
            ApiKey = Environment.GetEnvironmentVariable("ASPNETCORE_APIKEY");
            Client = new PushbulletClient(ApiKey, TimeZoneInfo.Local);
        }

        public void Send((string Rule, IfiEvent Event) ifiEvent)
        {
            
            PushLinkRequest reqeust;

            reqeust = new PushLinkRequest
            {
                ChannelTag = "ifibot",
                Title = ifiEvent.Item1 + " - " + ifiEvent.Item2.Name,
                Url = ifiEvent.Item2.URL
            };
            
            PushNote(reqeust);
            Console.WriteLine(DateTime.Now + " - " + ifiEvent.Rule + " - " + ifiEvent.Event.Name);
        }

        public void Send(IEnumerable<(string Rule, IfiEvent Event)> events)
        {
            foreach (var ifiEvent in events)
                Send(ifiEvent);
        }
      
        private void PushNote(PushLinkRequest request)
        {
            try
            {
                var response = Client.PushLink(request);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}