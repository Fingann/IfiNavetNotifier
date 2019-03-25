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
    public class NavetNotifier
    {
        private IEventClient WebClient { get; }
        private INotifyManager PushManager { get; }
        private ITaskScheduler TaskScheduler { get; set; }
        private BusinessRuleChecker RuleChecker { get; }
        private ILogger Logger { get; }
        private ConcurrentBag<IfiEvent> EventList { get; set; }
        
        public NavetNotifier(IEventClient webClient, INotifyManager pushManager,ITaskScheduler taskScheduler, ILogger logger)
        {
            TaskScheduler = taskScheduler;
            WebClient = webClient;
            PushManager = pushManager;
            Logger = logger;
            RuleChecker = new BusinessRuleChecker();
        }

        private async Task CheckEvents()
        {

            var newEventList = (await WebClient.GetEvents()).ToList();
            
            var flaggedEvents = RuleChecker.Enfocre(newEventList, EventList);
            PushManager.Send(flaggedEvents);

            EventList = new ConcurrentBag<IfiEvent>(newEventList);
        }

        public void Start()
        {
            InitializeList();
            TaskScheduler.Start(CheckEvents);
        }

       

        public void InitializeList()
        {
            Logger.Debug("Initializing List");
            EventList = new ConcurrentBag<IfiEvent>(WebClient.GetEvents().Result);          
            Logger.Debug("Initializing Complete");

        }
    }
}

