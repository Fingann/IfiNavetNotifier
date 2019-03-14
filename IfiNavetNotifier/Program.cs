using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using IfiNavetNotifier.Database;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            INotifyManager pushManager = new PushbulletManager();
            var cookieClient = new CookieClient();
            IEventClient eventClient = new EventWebClient(cookieClient);


            Login(cookieClient);
            var howOftenToRun = TimeSpan.FromSeconds(10);

            var context = new IfiEventContext();

            //setting up database

            var notifier = new Notifier(context, eventClient, pushManager);
            notifier.InitializeDb();

            notifier.Run(howOftenToRun);

            while (true) Thread.Sleep(1000 * 60 * 60);
        }

        public static void Login(CookieClient cookieClient)
        {
            var loginCredentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", Environment.GetEnvironmentVariable("ASPNETCORE_USER")),
                new KeyValuePair<string, string>("password", Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD"))
            };
            var content = new FormUrlEncodedContent(loginCredentials);

            var response = cookieClient.PostAsync("https://ifinavet.no/login", content).Result;
            //var res = response.Content.ReadAsStringAsync().Result;
        }
    }
}