using System;
namespace IfiNavetNotifier
{
    public class IfiEvent 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Link { get; set; }


        public override string ToString()
        {
            return $@"Name = {Name}
            Date = {Date}
            Food = {Food}
            PlacesLeft = {PlacesLeft}";
        }
    }
}
