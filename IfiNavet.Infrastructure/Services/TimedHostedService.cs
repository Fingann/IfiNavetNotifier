using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.Web;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Infrastructure.Extentions;
using IfiNavet.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IfiNavet.Infrastructure.Services
{
    public class TimedHostedService: IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private IfiNavetDbContext _context;
        private IEventClient _client;

        public TimedHostedService(ILogger<TimedHostedService> logger,IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            var scope = scopeFactory.CreateScope();
            
            _client = scope.ServiceProvider.GetRequiredService<IEventClient>();
            _context = scope.ServiceProvider.GetRequiredService<IfiNavetDbContext>();
      
            
            _client.LoggInn(new UserLogin(Environment.GetEnvironmentVariable("ASPNETCORE_SERVICEUSER"), Environment.GetEnvironmentVariable("ASPNETCORE_SERVICEPASS")));

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(20));

            return Task.CompletedTask;
        }

        

        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
            //var dbEvents = await _context.IfiEvents.ToListAsync();
            var webEvents = await _client.GetEvents();
            var ifievents = webEvents.ToList();
            foreach (var ifievent in ifievents)
            {
                _context.AddOrUpdate(ifievent);
                
                
            }
            _context.SaveChanges(); 

            _logger.LogInformation($"Timed Background Service: Updated db, Number:{ifievents.Count}");

            //IfiEventComparer comparer = new IfiEventComparer();
            //var changed = comparer.Compare(dbEvents, webEvents);





        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}