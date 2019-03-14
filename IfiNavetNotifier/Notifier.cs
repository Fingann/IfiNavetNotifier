using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavetNotifier.BusinessRules;
using IfiNavetNotifier.Database;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    public class Notifier
    {
        public Notifier(IfiEventContext context, IEventClient webParser, INotifyManager pushManager)
        {
            Context = context;
            WebParser = webParser;
            PushManager = pushManager;
            BusinessRuleChecker = new BusinessRuleChecker();
        }

        private IEventClient WebParser { get; }

        private IfiEventContext Context { get; }
        private INotifyManager PushManager { get; }
        public BusinessRuleChecker BusinessRuleChecker { get; set; }


        public async Task CheckEvents()
        {
            var ifiEvents = await WebParser.GetEvents();
            if (ifiEvents == null) return;

            var dbevents = Context.IfiEvent.ToList();


            var flagedEvents = BusinessRuleChecker.Enfocre(ifiEvents.ToList(), dbevents).ToList();

    
            if (flagedEvents.Any())
                foreach (var ifiEvent in flagedEvents)
                    PushManager.Send(ifiEvent);

            Context.RemoveRange(dbevents);
            await Context.SaveChangesAsync();
            Context.AddRange(ifiEvents);
            await Context.SaveChangesAsync();

            Console.WriteLine(DateTime.Now+$" - Events flaged: {flagedEvents.Count}");
        }

        public void Run(TimeSpan periodTimeSpan)
        {
            Console.WriteLine(DateTime.Now +" - Starting pariodic task with interval of: "+ periodTimeSpan);

            var ct = new CancellationToken();          
            PeriodicTask(CheckEvents, periodTimeSpan, ct);
        }

        public async void PeriodicTask(Func<Task> task,TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await task();
                await Task.Delay(interval, cancellationToken);
            }
        }

        public void InitializeDb()
        {
            Console.WriteLine(DateTime.Now +" - Initializing DB");
            Context.AddRange(WebParser.GetEvents().Result);
            Context.SaveChanges();
            Console.WriteLine(DateTime.Now +" - Initializing Complete");

        }
    }
}