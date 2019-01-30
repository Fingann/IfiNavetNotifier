using System;
using System.Collections.Generic;

namespace IfiNavetNotifier
{
    public class TestCompare
    {
        public TestCompare()
        {

            var comparer = new ListComparer();
            List<IfiEvent> db = new List<IfiEvent>()
            {
                new IfiEvent { Name = "Fake1", Date = DateTime.Now, Food = "yes", PlacesLeft = 200, Link = "https:/ifinavet.no/event/221" },
                new IfiEvent { Name = "Fake2", Date = DateTime.Now, Food = "yes", PlacesLeft = 50, Link = "https:/ifinavet.no/event/222" },
                new IfiEvent { Name = "Fake3", Date = DateTime.Now, Food = "yes", PlacesLeft = 78, Link = "https:/ifinavet.no/event/223" },
                new IfiEvent { Name = "Fake4", Date = DateTime.Now, Food = "yes", PlacesLeft = 0, Link = "https:/ifinavet.no/event/224" },
                new IfiEvent { Name = "Fake5", Date = DateTime.Now, Food = "yes", PlacesLeft = 0, Link = "https:/ifinavet.no/event/225" },
                new IfiEvent { Name = "Fake6", Date = DateTime.Now, Food = "yes", PlacesLeft = 1, Link = "https:/ifinavet.no/event/226" }

            };
            List<IfiEvent> web = new List<IfiEvent>()
            {
                new IfiEvent { Name = "Fake1", Date = DateTime.Now, Food = "yes", PlacesLeft = 200, Link = "https:/ifinavet.no/event/221" },
                new IfiEvent { Name = "Fake2", Date = DateTime.Now, Food = "yes", PlacesLeft = 50, Link = "https:/ifinavet.no/event/222" },
                new IfiEvent { Name = "Fake3", Date = DateTime.Now, Food = "yes", PlacesLeft = 78, Link = "https:/ifinavet.no/event/223" },
                new IfiEvent { Name = "Fake4", Date = DateTime.Now, Food = "yes", PlacesLeft = 6, Link = "https:/ifinavet.no/event/224" },
                new IfiEvent { Name = "Fake5", Date = DateTime.Now, Food = "yes", PlacesLeft = 0, Link = "https:/ifinavet.no/event/225" },
                new IfiEvent { Name = "Fake6", Date = DateTime.Now, Food = "yes", PlacesLeft = 0, Link = "https:/ifinavet.no/event/226" }
            };

            var diff = comparer.Compare(web, db);

            diff.ForEach(Console.WriteLine);


        }
    }
}
