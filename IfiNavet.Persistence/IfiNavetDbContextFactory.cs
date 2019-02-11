using IfiNavet.Persistence.Infastructure;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Persistence
{
    public class IfiNavetDbContextFactory: DesignTimeDbContextFactoryBase<IfiNavetDbContext>
    {
        protected override IfiNavetDbContext CreateNewInstance(DbContextOptions<IfiNavetDbContext> options)
        {
            return new IfiNavetDbContext(options);
        }
    }
}