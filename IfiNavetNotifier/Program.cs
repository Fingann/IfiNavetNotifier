using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using IfiNavetNotifier.Logger;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Web;

namespace IfiNavetNotifier
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SetCulture();
            ILogger logger = new ConsoleLogger(ConsoleLogger.LoggLevel.Debugg);

            var cookieClient = new HttpCookieClient();
            Login(cookieClient);

            INotifyManager pushManager = new PushbulletManager();
            IEventClient eventClient = new EventWebClient(cookieClient, logger);

            var howOftenToRun = TimeSpan.FromSeconds(10);
            var notifier = new Notifier(eventClient, pushManager,logger);


            notifier.InitializeDb();
            notifier.Run(howOftenToRun);

            while (true) Thread.Sleep(1000 * 60 * 60);
        }

        private static void SetCulture()
        {
            var culture = CultureInfo.CreateSpecificCulture("no");
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public static void Login(HttpCookieClient httpCookieClient)
        {
            Console.WriteLine(DateTime.Now +" - Logging in and storing credentials");
            var loginCredentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", Environment.GetEnvironmentVariable("ASPNETCORE_USER")),
                new KeyValuePair<string, string>("password", Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD"))
            };
            var content = new FormUrlEncodedContent(loginCredentials);

            var response = httpCookieClient.PostAsync("https://ifinavet.no/login", content).Result;
            //var res = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(DateTime.Now +" - Login complete");

        }
    }
}