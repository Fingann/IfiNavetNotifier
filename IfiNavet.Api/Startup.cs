using System.Reflection;
using IfiNavet.Application.Events.Queries.GetEvent;
using IfiNavet.Application.Events.Queries.GetEventsList;
using IfiNavet.Application.Notifications;
using IfiNavet.Application.Web;
using IfiNavet.Infrastructure.Notifications;
using IfiNavet.Infrastructure.Services;
using IfiNavet.Infrastructure.Web;
using IfiNavet.Persistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace IfiNavet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
           
            services.AddMediatR(typeof(GetEventListQueryHandler).GetTypeInfo().Assembly);  
            services.AddMediatR(typeof(GetEventQueryHandler).GetTypeInfo().Assembly);  

            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IfiNavetDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IEventClient,EventWebClient>();
            
            services.AddHostedService<TimedHostedService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Events}/{action=Get}");
            });;
        }
    }
}