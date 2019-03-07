using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public class TrackableBase : ITrackable
    {
        public TrackableBase()
        {
            CreatedDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; }
        public DateTime? ModifiedDate { get; set; }
    }
}