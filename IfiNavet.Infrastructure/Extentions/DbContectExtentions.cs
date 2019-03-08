using System;
using System.Linq;
using IfiNavet.Core.Entities.Events;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Infrastructure.Extentions
{
    public static class DbContectExtentions
    {
        public static void AddOrUpdate(this DbContext ctx, object entity)
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static void DetachLocal<T>(this DbContext context, T t, string entryId) 
            where T : IfiEvent 
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Link.Equals(entryId));
            if (local == new IfiEvent())
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}