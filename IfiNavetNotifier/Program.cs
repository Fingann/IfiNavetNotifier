using System;
using System.Collections.Generic;
using System.Globalization;
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
            var culture = CultureInfo.CreateSpecificCulture("no");
            Thread.CurrentThread.CurrentCulture = culture;

          

            var cookieClient = new CookieClient();
            Login(cookieClient);

            INotifyManager pushManager = new PushbulletManager();
            IEventClient eventClient = new EventWebClient(cookieClient);
            var context = new IfiEventContext();

            var howOftenToRun = TimeSpan.FromSeconds(10);
            var notifier = new Notifier(context, eventClient, pushManager);


            notifier.InitializeDb();
            notifier.Run(howOftenToRun);

            while (true) Thread.Sleep(1000 * 60 * 60);
        }

        public static void Login(CookieClient cookieClient)
        {
            Console.WriteLine(DateTime.Now +" - Logging in and storing credentials");
            var loginCredentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", Environment.GetEnvironmentVariable("ASPNETCORE_USER")),
                new KeyValuePair<string, string>("password", Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD"))
            };
            var content = new FormUrlEncodedContent(loginCredentials);

            var response = cookieClient.PostAsync("https://ifinavet.no/login", content).Result;
            //var res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(DateTime.Now +" - Login complete");

        }
    }
}