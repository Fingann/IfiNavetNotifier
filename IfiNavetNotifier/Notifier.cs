using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using IfiNavetNotifier.Database;

namespace IfiNavetNotifier
{
    public class Notifier
    {
       
        public IfiWebsiteParser WebParser { get; set; }
        public IfiEventContext Context { get; set; }
        public MailClient MailClient { get; set; }
        public List<string> Emails { get; set; }
        public ListComparer Listcomparer { get; set; }
        public Notifier(IfiEventContext context, MailClient mailClient,IEnumerable<string> emails)
        {
            Context = context;
            WebParser = new IfiWebsiteParser();
            MailClient = new MailClient();
            Emails = emails.ToList();
            Listcomparer = new ListComparer();

        }

        public void Run()
        {
           
           
            Initialize();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(20);

            var timer = new Timer((e) =>
            {
                var eventids = WebParser.GetEventIds();
                var ifiEvents = WebParser.GetEvents(eventids).ToList();
                var dbevents = Context.IfiEvent.ToList();

                var diffrent = Listcomparer.Compare(ifiEvents, dbevents);
               
                if (diffrent.Any())
                {
                    MailClient.Send(diffrent, Emails);
                    Context.RemoveRange(dbevents);
                    Context.SaveChanges();
                    Context.Add(ifiEvents);
                    Context.SaveChanges();

                }
                Console.WriteLine($"Ran updates: {diffrent.Count()}");
            }, null, startTimeSpan, periodTimeSpan);
        }

        private void Initialize() { 
            var eventids = WebParser.GetEventIds();
            var ifiEvents = WebParser.GetEvents(eventids).ToList();
            Context.AddRange(ifiEvents);
            Context.SaveChanges();
        }







    }




}
