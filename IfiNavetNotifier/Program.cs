using System;
using System.Collections.Generic;
using System.Threading;
using IfiNavetNotifier.Database;

namespace IfiNavetNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> emails = new List<string> { "fingann92@gmail.com", "josteinmeg@gmail.com" };


            MailClient mailClient = new MailClient();
            //var t = new List<IfiEvent> {new IfiEvent { Name = "Fake1", Date = DateTime.Now, Food = "yes", PlacesLeft = 5 },
            //new IfiEvent { Name = "Fake2", Date = DateTime.Now, Food = "yes", PlacesLeft = 5 } };
            //mailClient.Send(t,emails);

            using (var context = new IfiEventContext())
            {
                Notifier notifier = new Notifier(context, mailClient,emails);

                notifier.Run();

                Console.ReadKey();
            }

        }
    }
}
