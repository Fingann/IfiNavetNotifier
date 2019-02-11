using System;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Persistence
{
    public class IfiNavetDbContext: DbContext
    {
        public IfiNavetDbContext(DbContextOptions<IfiNavetDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<IfiEvent> IfiEvents { get; set; }
    }
}