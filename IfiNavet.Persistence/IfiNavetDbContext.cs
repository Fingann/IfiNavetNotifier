using System;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IfiEvent>().HasKey(x => x.Link);
            modelBuilder.Entity<UserLogin>().HasData(new UserLogin("sodnrefi", "ifibot123"));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IfiNavetDbContext).Assembly);

        }

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<IfiEvent> IfiEvents { get; set; }
        
        
        }
    }
