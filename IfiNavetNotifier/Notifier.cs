using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavetNotifier.BusinessRules;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    public class Notifier
    {
        private IEventClient WebParser { get; }
        private INotifyManager PushManager { get; }
        private BusinessRuleChecker BusinessRuleChecker { get; }

        public ConcurrentBag<IfiEvent> EventList { get; set; }
        
        public Notifier(IEventClient webParser, INotifyManager pushManager)
        {
            WebParser = webParser;
            PushManager = pushManager;
            BusinessRuleChecker = new BusinessRuleChecker();
        }

        public async Task CheckEvents()
        {

            var ifiEvents = await WebParser.GetEvents();
            if (ifiEvents == null) return;
            
            var flagedEvents = BusinessRuleChecker.Enfocre(ifiEvents, EventList).ToList();
    
            if (flagedEvents.Any())
                   foreach (var ifiEvent in flagedEvents)
                        PushManager.Send(ifiEvent);

            EventList = new ConcurrentBag<IfiEvent>(ifiEvents);
            Console.WriteLine(DateTime.Now +" - Check Event finished");
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
            Console.WriteLine(DateTime.Now +" - Initializing List");
            EventList = new ConcurrentBag<IfiEvent>(WebParser.GetEvents().Result);          
            Console.WriteLine(DateTime.Now +" - Initializing Complete");

        }
    }
}

