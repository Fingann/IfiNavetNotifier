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

  
        public void Send(Tuple<string, IfiEvent> ifiEvent)
        {
            PushNoteRequest reqeust;

            reqeust = new PushNoteRequest
            {
                ChannelTag = "ifibot",
                Title = ifiEvent.Item1 + " - " + ifiEvent.Item2.Name,
                Body = "Link: " + ifiEvent.Item2.Link
            };
            PushNote(reqeust);
        }

        public void Send(IEnumerable<IfiEvent> events)
        {
            PushNoteRequest reqeust;

            if (events.Count() == 1)
            {
                reqeust = new PushNoteRequest
                {
                    ChannelTag = "ifibot",
                    Title = $"{events.First().PlacesLeft} - {events.First().Name}",
                    Body = events.First().ToString()
                };
            }
            else
            {
                var body = string.Empty;
                foreach (var ifiEvent in events)
                    body += ifiEvent.Name + Environment.NewLine + "Places Left = " + ifiEvent.Name +
                            Environment.NewLine + ifiEvent + Environment.NewLine;
                reqeust = new PushNoteRequest
                {
                    ChannelTag = "ifibot",
                    Title = $"{events.Count()} events updated",
                    Body = body
                };
            }

            PushNote(reqeust);
        }

        private void PushNote(PushNoteRequest request)
        {
            try
            {
                var response = Client.PushNote(request);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}