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
using IfiNavetNotifier.Notifications;

namespace IfiNavetNotifier
{
    public class Notifier
    {
       
        public IWebParser<IfiEvent> WebParser { get; set; }
        public IfiEventContext Context { get; set; }
        public INotifyManager PushManager { get; set; }
        public List<string> Emails { get; set; }
        public ListComparer Listcomparer { get; set; }

        public Notifier(IfiEventContext context, IWebParser<IfiEvent> webParser, INotifyManager pushManager)
        {
            Context = context;
            WebParser = webParser;
            PushManager = pushManager;
            
            Listcomparer = new ListComparer();
            InitializeDB();

        }

        public async Task PeriodicTask(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await CheckEvents();
                await Task.Delay(interval, cancellationToken);
            }
        }

        public async Task CheckEvents()
        {
                var ifiEvents = WebParser.GetEntitys().ToList();
                if (ifiEvents == null) return;

                var dbevents = Context.IfiEvent.ToList();
                var diffrent = Listcomparer.Compare(ifiEvents, dbevents);

                if (diffrent.Any())
                {
                    PushManager.Send(diffrent);
                    Context.RemoveRange(dbevents);
                    await Context.SaveChangesAsync();
                    Context.AddRange(ifiEvents);
                    await Context.SaveChangesAsync();

                }
                Console.WriteLine($"Ran updates: {diffrent.Count()}");
        }

        public async void Run()
        {
            var periodTimeSpan = TimeSpan.FromSeconds(20);
            CancellationToken ct = new CancellationToken();
            await PeriodicTask(periodTimeSpan, ct);
        }


        private void InitializeDB() { 

            Context.AddRange(WebParser.GetEntitys());
            Context.SaveChanges();
        }







    }




}
