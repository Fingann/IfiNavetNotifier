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
using IfiNavetNotifier.Pushbullet;

namespace IfiNavetNotifier
{
    public class Notifier
    {
       
        public IfiWebsiteParser WebParser { get; set; }
        public IfiEventContext Context { get; set; }
        public PushbulletManager PushManager { get; set; }
        public List<string> Emails { get; set; }

        public ListComparer Listcomparer { get; set; }

        public Notifier(IfiEventContext context, PushbulletManager pushManager)
        {
            Context = context;
            WebParser = new IfiWebsiteParser();
            PushManager = pushManager;
            
            Listcomparer = new ListComparer();
            InitializeDB();

        }

        public async Task PeriodicFooAsync(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await CheckEvents();
                await Task.Delay(interval, cancellationToken);
            }
        }

        public async Task CheckEvents()
        {
                var ifiEvents = WebParser.GetEvents().ToList();
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
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(20);
            CancellationToken ct = new CancellationToken();
            await PeriodicFooAsync(periodTimeSpan, ct);




            //var timer = new Timer((e) =>
            //{

            //    var ifiEvents = WebParser.GetEvents().ToList();
            //    var dbevents = Context.IfiEvent.ToList();

            //    var diffrent = Listcomparer.Compare(ifiEvents, dbevents);

            //    if (diffrent.Any())
            //    {
            //        PushManager.Send(diffrent);
            //        Context.RemoveRange(dbevents);
            //        Context.SaveChanges();
            //        Context.Add(ifiEvents);
            //        Context.SaveChanges();

            //    }
            //    Console.WriteLine($"Ran updates: {diffrent.Count()}");
            //}, null, startTimeSpan, periodTimeSpan);
        }

        private void InitializeDB() { 
            Context.AddRange(WebParser.GetEvents());
            Context.SaveChanges();
        }







    }




}
