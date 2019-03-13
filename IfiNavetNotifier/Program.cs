using System;
using System.Threading;
using System.Threading.Tasks;
using IfiNavetNotifier.Database;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestCompare tc = new TestCompare();

            INotifyManager pushManager = new PushbulletManager();
            IEventClient webParser = new EventWebClient();
            IListComparer comparer = new ListComparer();
            
            
            webParser.LoggInn(new UserLogin(
                Environment.GetEnvironmentVariable("ASPNETCORE_USER"),
                Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD")
                ));
            
            TimeSpan howOftenToRun = TimeSpan.FromSeconds(10);

            var context = new IfiEventContext();
           
                //setting up database
               
                Notifier notifier = new Notifier(context, webParser, pushManager, comparer);
                notifier.InitializeDb();
             
                notifier.Run(howOftenToRun);

                while (true)
                {
                    Thread.Sleep(1000*60*60);
                }
            
        }

        }
    }

