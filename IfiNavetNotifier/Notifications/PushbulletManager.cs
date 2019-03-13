using System;
using System.Collections.Generic;
using System.Linq;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Requests.Ephemerals;

namespace IfiNavetNotifier.Notifications
{
    public class PushbulletManager : INotifyManager
    {
        public PushbulletClient Client { get; set; }
        public string ApiKey { get; set; }

        public PushbulletManager()
        {
            ApiKey = "o.Zww8BIzXgLyxQobNNIUDPAEbgkSQYYDf";
            Client = new PushbulletClient(ApiKey, TimeZoneInfo.Local);
        }

        public void Send(IEnumerable<IfiEvent> events)
        {
           
            

            PushNoteRequest reqeust;
            
            if (events.Count() == 1)
            {
                reqeust = new PushNoteRequest()
                {
                    ChannelTag = "ifibot",
                    Title = $"{events.First().PlacesLeft} - {events.First().Name}",
                    Body = events.First().ToString()
                };
            }
            else
            {
                string body = string.Empty;
                foreach (var ifiEvent in events)
                {
                    body += ifiEvent.Name+ Environment.NewLine+ ifiEvent + Environment.NewLine;
                }
                reqeust = new PushNoteRequest()
                {
                    ChannelTag = "ifibot",
                    Title = $"{events.Count()} events updated",
                    Body = body
                };

            }

            try
            {
                var response = Client.PushNote(reqeust);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
