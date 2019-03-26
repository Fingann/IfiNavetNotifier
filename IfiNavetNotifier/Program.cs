using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using IfiNavetNotifier.Logger;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.TaskScheduler;
using IfiNavetNotifier.Web;
using PushbulletSharp;

namespace IfiNavetNotifier
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PushbulletClient pm = new PushbulletClient();
            
            SetCulture();
            ILogger logger = new ConsoleLogger(ConsoleLogger.LoggLevel.Debugg);
            
            var howOftenToRun = TimeSpan.FromSeconds(10);
            ITaskScheduler taskScheduler = new PeriodicTaskScheduler(howOftenToRun);
       
            var cookieClient = new HttpCookieClient();
            Login(cookieClient, logger);
            
            IEventClient eventClient = new EventWebClient(cookieClient, logger);
            INotifyManager pushManager = new PushbulletManager(logger);
            
            var notifier = new NavetNotifier(eventClient, pushManager, taskScheduler,logger);
            notifier.Start();

            while (true) Thread.Sleep(1000 * 60 * 60);
        }

        private static void SetCulture()
        {
            var culture = CultureInfo.CreateSpecificCulture("no");
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public async static void Login(HttpCookieClient httpCookieClient, ILogger logger)
        {
            logger.Informtion("Logging in and storing credentials");
            var loginCredentials = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", Environment.GetEnvironmentVariable("ASPNETCORE_USER")),
                new KeyValuePair<string, string>("password", Environment.GetEnvironmentVariable("ASPNETCORE_PASSWORD"))
            };
            await httpCookieClient.Login(loginCredentials);
            //var res = response.Content.ReadAsStringAsync().Result;
            logger.Informtion("Loggin Complete");

        }
    }
}