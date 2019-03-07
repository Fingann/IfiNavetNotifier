using System;

namespace IfiNavet.Application.Events.Queries.GetEvent
{
    public class EventViewModel
    {
        private DateTime _date;
        public string Name { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }

        public string Date {get;set;}

        public bool Open { get; set; }

    }
}

