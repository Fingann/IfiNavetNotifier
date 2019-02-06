using System;
namespace IfiNavet.Core.Entities
{
    public class IfiEvent
    {
        public IfiEvent()
        {
        }

        public string Name { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        public Uri Link { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }
    }
}
