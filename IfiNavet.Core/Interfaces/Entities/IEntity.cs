using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        
    }
}