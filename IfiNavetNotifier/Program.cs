using System;
using System.Collections.Generic;
using System.Threading;
using IfiNavetNotifier.Database;
using IfiNavetNotifier.Notifications;
using IfiNavetNotifier.Test;

namespace IfiNavetNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestCompare tc = new TestCompare();

            INotifyManager pushManager = new PushbulletManager();
            IWebParser<IfiEvent> webParser = new IfiWebsiteParser();

            using (var context = new IfiEventContext())
            {
                Notifier notifier = new Notifier(context, webParser, pushManager);

                notifier.Run();

                while (true)
                {
                    Thread.Sleep(1000*60*60);
                }
            }
        }

        }
    }

