using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.Web;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Infrastructure.Extentions;
using IfiNavet.Persistence;
using InfluxDB.Collector;
using InfluxDB.LineProtocol.Client;
using InfluxDB.LineProtocol.Payload;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IfiNavet.Infrastructure.Services
{
    public class TimedHostedService: IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private IfiNavetDbContext _context;
        private IEventClient _client;
        public LineProtocolClient LineProtocolClient { get; set; }

        public TimedHostedService(ILogger<TimedHostedService> logger,IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            var scope = scopeFactory.CreateScope();
            
            _client = scope.ServiceProvider.GetRequiredService<IEventClient>();
            _context = scope.ServiceProvider.GetRequiredService<IfiNavetDbContext>();
      
            
            _client.LoggInn(new UserLogin(Environment.GetEnvironmentVariable("ASPNETCORE_SERVICEUSER"), Environment.GetEnvironmentVariable("ASPNETCORE_SERVICEPASS")));

            LineProtocolClient = new LineProtocolClient(new Uri("http://localhost:8086"), "IfiNavet");
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        

        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
            //var dbEvents = await _context.IfiEvents.ToListAsync();
            var webEvents = await _client.GetEvents();
            var ifievents = webEvents.Where(x=> x.Date > DateTime.Now).ToList();
             var points = new List<LineProtocolPoint>();
            foreach (var ifievent in ifievents)
            {
                var json = JsonConvert.SerializeObject(ifievent);
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, Object>>(json);

                points.Add(new LineProtocolPoint(ifievent.Link, new Dictionary<string, object>
                {
                    { "PlacesLeft", ifievent.PlacesLeft }
                },new Dictionary<string, string>
                {
                    { "Name", ifievent.Name },
                    { "Date", ifievent.Date.ToUniversalTime().ToString(CultureInfo.CurrentCulture) }
                },DateTime.UtcNow));

            }
            var payload = new LineProtocolPayload();
            foreach (var point in points)
            {
                payload.Add(point);

            }
            var influxResult = await LineProtocolClient.WriteAsync(payload);
            if (!influxResult.Success)
                Console.Error.WriteLine(influxResult.ErrorMessage);

//            foreach (var ifievent in ifievents)
//            {
//                var dbEvent = _context.IfiEvents.AsNoTracking().FirstOrDefault(x => x.Link == ifievent.Link);
//                if (dbEvent == null )
//                {
//                    _context.IfiEvents.Add(ifievent);
//                    _context.Entry(ifievent).State = EntityState.Detached;
//                    continue;
//                }
//                if (!dbEvent.Equals(ifievent))
//                {
//                    _context.IfiEvents.Update(ifievent);
//                    _context.Entry(ifievent).State = EntityState.Modified;
//
//                    continue;
//                    
//                }
//
//                
//            }
//            

//            var temp = _context.ChangeTracker.Entries();
//            foreach (var entry in temp)
//            {
//                if (entry.State != EntityState.Modified)
//                {
//                    _context.IfiEvents.State = EntityState.Detached;
//
//                }
//
//            }
           // _context.SaveChanges();
            _logger.LogInformation($"Timed Background Service: Updated db, Number:{ifievents.Count}");

          
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