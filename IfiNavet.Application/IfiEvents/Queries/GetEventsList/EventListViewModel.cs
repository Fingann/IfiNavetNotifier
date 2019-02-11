using System;

namespace IfiNavet.Application.IfiEvents.Queries.GetEventsList
{
    public class EventListViewModel
    {
        public string Name { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        public Uri Link { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }

    }
}

