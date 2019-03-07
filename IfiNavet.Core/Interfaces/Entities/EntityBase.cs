using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public abstract class EntityBase<T> : TrackableBase, IEntity<T>
    {
        public T Id { get; set; }
      
    }

    
}