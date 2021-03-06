using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTnet.Client;
using CommandMicroservice.Services;
using CommandMicroservice.Hubs;
using CommandMicroservice.Configuration;

namespace CommandMicroservice
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
            services.Configure<ConfigurationSettings>(Configuration.GetSection(nameof(ConfigurationSettings)));
            services.AddCors(setupAction =>
            {
                setupAction.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://dashboard:3000","http://apigateway:80").AllowCredentials();
                });
            });
			
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Command Microservice",
                    Version = "v1",
                    Description = "Command Microservice is listening for events and sends commands to virtual actuators."
                });
                c.EnableAnnotations();
            });

            services.AddHttpClient<ActuatorClient>();
            services.AddSingleton<IMqttSubscriber,MqttSubscriber>();
            services.AddSignalR();
            services.AddTransient<INotificationService, NotificationService>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommandMicroservice v1"));
            }

            app.ApplicationServices.GetService<IMqttSubscriber>();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("/notifications");
            });
        }
    }
}
