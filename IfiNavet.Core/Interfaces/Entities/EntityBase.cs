using System;

namespace IfiNavet.Core.Interfaces.Entities
{
    public abstract class EntityBase<T> : IEntity<T>
    {
        public EntityBase()
        {
            CreatedDate = DateTime.Now;
        }
        public T Id { get; set; }
        public DateTime CreatedDate { get;}
        public DateTime? ModifiedDate { get; set; }
    }
}