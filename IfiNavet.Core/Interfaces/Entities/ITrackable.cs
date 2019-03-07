using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public interface ITrackable
    {
        DateTime CreatedDate { get; }
        DateTime? ModifiedDate { get; set; }
    }
}