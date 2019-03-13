using System;
using System.Collections.Generic;
using System.Net.Http;
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
         
            Login();
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

        public static void Login()
        {
            
            var loginCredentials = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("username", Environment.GetEnvironmentVariable("ASPNETCORE_USER")),
                new KeyValuePair<string, string>("password", Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD"))
                
            };
            var content = new FormUrlEncodedContent(loginCredentials);

            var response =StaticHttpClient.Client.PostAsync("https://ifinavet.no/login", content).Result;
            var res = response.Content.ReadAsStringAsync().Result;

        }

        }
    }

