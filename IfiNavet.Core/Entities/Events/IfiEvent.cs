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
        public override bool Equals(object obj)
        {
            if (!(obj is IfiEvent))
                return false;
            IfiEvent ifievent = (IfiEvent) obj;
            
            if (Link != ifievent.Link)
                return false;
            if (PlacesLeft != ifievent.PlacesLeft)
                return false;
            if (Open != ifievent.Open)
                return false;
            if (Name != ifievent.Name)
                return false;
            if (Food != ifievent.Food)
                return false;
            if (Location != ifievent.Location)
                return false;
            if (Date != ifievent.Date)
                return false;
           
            return true;
        }
    }
}
