using System;
using System.Collections.Generic;
using System.Linq;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Requests.Ephemerals;

namespace IfiNavetNotifier.Pushbullet
{
    public class PushbulletManager
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
            string body = string.Empty;
            foreach (var ifiEvent in events)
            {
                body += ifiEvent + Environment.NewLine;
            }
            var plainTextContent = body;

            PushNoteRequest reqeust = new PushNoteRequest()
            {
                ChannelTag="ifibot",
                Title = $"{events.Count()} events updated",
                Body = body
            };

            var response = Client.PushNote(reqeust);
        }
    }
}
