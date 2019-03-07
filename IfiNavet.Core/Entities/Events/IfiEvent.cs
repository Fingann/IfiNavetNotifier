using System;
using IfiNavet.Core.Interfaces.Entities;



namespace IfiNavet.Core.Entities.Events
{
    public class IfiEvent : TrackableBase
    {
        public string Name { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        
        public string Link { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }
    }
}
