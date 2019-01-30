using System;
using System.Collections.Generic;
using System.Threading;
using IfiNavetNotifier.Database;
using IfiNavetNotifier.Pushbullet;

namespace IfiNavetNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> emails = new List<string> { "fingann92@gmail.com", "josteinmeg@gmail.com" };


            PushbulletManager pushManager = new PushbulletManager();
            //var t = new List<IfiEvent> {new IfiEvent { Name = "Fake1", Date = DateTime.Now, Food = "yes", PlacesLeft = 5 },
            //new IfiEvent { Name = "Fake2", Date = DateTime.Now, Food = "yes", PlacesLeft = 5 } };
            //mailClient.Send(t,emails);



            using (var context = new IfiEventContext())
            {
                Notifier notifier = new Notifier(context, pushManager);

                notifier.Run();

                while (true)
                {
                    Thread.Sleep(5000);
                }
            }
        }

        }
    }

