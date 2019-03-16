using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavetNotifier.BusinessRules;
using IfiNavetNotifier.Logger;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.TaskScheduler;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    public class Notifier
    {
        private IEventClient WebClient { get; }
        private INotifyManager PushManager { get; }
        private ITaskScheduler TaskScheduler { get; set; }
        private BusinessRuleChecker BusinessRuleChecker { get; }
        private ILogger Logger { get; }
        private ConcurrentBag<IfiEvent> EventList { get; set; }
        
        public Notifier(IEventClient webClient, INotifyManager pushManager,ITaskScheduler taskScheduler, ILogger logger)
        {
            TaskScheduler = taskScheduler;
            WebClient = webClient;
            PushManager = pushManager;
            Logger = logger;
            BusinessRuleChecker = new BusinessRuleChecker();
        }

        private async Task CheckEvents()
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

        public void Start()
        {
            TaskScheduler.Start(CheckEvents);
        }

       

        public void InitializeDb()
        {
            Logger.Debug("Initializing List");
            EventList = new ConcurrentBag<IfiEvent>(WebClient.GetEvents().Result);          
            Logger.Debug("Initializing Complete");

        }
    }
}

