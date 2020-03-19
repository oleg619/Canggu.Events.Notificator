using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using CangguEvents.Asp.Configurations;
using CangguEvents.Asp.Helpers;
using CangguEvents.Asp.Middleware;
using CangguEvents.Asp.Models.Commands;
using CangguEvents.Asp.Services;
using CangguEvents.Asp.Services.Implementation;
using CangguEvents.SQLite;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using CorsMiddleware = CangguEvents.Asp.Middleware.CorsMiddleware;

namespace CangguEvents.Asp
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
            services.AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddNewtonsoftJson();
            services.AddOptions();

            services.AddDbContext<EventsContext>(options => options.UseSqlite(
                Configuration.GetConnectionString("EventDb")));

            services.AddControllers();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorsMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseMiddleware<SerilogMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<TelegramHostedService>()
                .As<IHostedService>()
                .InstancePerDependency();

            var config = GetBotConfiguration();
            ConfigureTelegram(builder, config);
            builder.RegisterAutomapper();

            builder.RegisterType<TelegramMessengerSender>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<SqlLiteRepository>().AsImplementedInterfaces();
            builder.RegisterType<TelegramMessageHandler>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<MessageParser>().AsSelf();

            builder.RegisterInstance(config).SingleInstance();
            builder.RegisterInstance(Log.Logger).AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterModule<MediatorModule>();
        }

        protected virtual void ConfigureTelegram(ContainerBuilder app, BotConfiguration configuration)
        {
            app.RegisterTelegram(configuration);
        }

        private BotConfiguration GetBotConfiguration()
        {
            var config = new BotConfiguration();

            Configuration.GetSection("BotConfiguration").Bind(config);
            return config;
        }
    }

    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(MessageCommandBase).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register all the ExceptionHandler classes (they implement IRequestExceptionHandler) in assembly holding the ExceptionHandler

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : null;
            });
        }
    }
}