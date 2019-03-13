using Microsoft.EntityFrameworkCore;

namespace IfiNavetNotifier.Database
{
    public class IfiEventContext : DbContext
    {
        public DbSet<IfiEvent> IfiEvent { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ifiNavet");
        }
    }
}