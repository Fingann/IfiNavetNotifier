using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavetNotifier.BusinessRules;
using IfiNavetNotifier.Logger;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    public class Notifier
    {
        private IEventClient WebClient { get; }
        private INotifyManager PushManager { get; }
        private BusinessRuleChecker BusinessRuleChecker { get; }
        private ILogger Logger { get; }
        public ConcurrentBag<IfiEvent> EventList { get; set; }
        
        public Notifier(IEventClient webClient, INotifyManager pushManager, ILogger logger)
        {
            
            WebClient = webClient;
            PushManager = pushManager;
            this.Logger = logger;
            BusinessRuleChecker = new BusinessRuleChecker();
        }

        public async Task CheckEvents()
        {

            var ifiEvents = await WebClient.GetEvents();
            if (ifiEvents == null) return;
            
            var flagedEvents = BusinessRuleChecker.Enfocre(ifiEvents, EventList).ToList();
    
            if (flagedEvents.Any())
                   foreach (var ifiEvent in flagedEvents)
                        PushManager.Send(ifiEvent);

            EventList = new ConcurrentBag<IfiEvent>(ifiEvents);
            Logger.Debug("Event finished");
        }

        public void Run(TimeSpan periodTimeSpan)
        {
            Logger.Debug("Starting pariodic task with interval of: "+ periodTimeSpan);

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
            Logger.Debug("Initializing List");
            EventList = new ConcurrentBag<IfiEvent>(WebClient.GetEvents().Result);          
            Logger.Debug("Initializing Complete");

        }
    }
}

