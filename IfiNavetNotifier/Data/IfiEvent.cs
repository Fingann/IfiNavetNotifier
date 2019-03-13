using System;
using System.ComponentModel.DataAnnotations;

namespace IfiNavetNotifier
{
    public class IfiEvent 
    {
        [Key]
        public string Link { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        public bool Open { get; set; }



        public override string ToString()
        {
            return Name + " - " + Date;
        }
    }
}
