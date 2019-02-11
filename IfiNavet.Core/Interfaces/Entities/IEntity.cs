using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedDate { get; }
        DateTime? ModifiedDate { get; set; }
    }
}