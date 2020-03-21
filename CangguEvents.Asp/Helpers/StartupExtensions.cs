using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using CangguEvents.Asp.Configurations;
using CangguEvents.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MihaZupan;
using Serilog;
using Telegram.Bot;

namespace CangguEvents.Asp.Helpers
{
    public static class StartupExtensions
    {
        public static void RegisterTelegram(this ContainerBuilder builder, BotConfiguration config)
        {
            var client = string.IsNullOrEmpty(config.Socks5Host)
                ? new TelegramBotClient(config.BotToken)
                : new TelegramBotClient(config.BotToken, new HttpToSocks5Proxy(config.Socks5Host, config.Socks5Port));

            builder.RegisterInstance(client).AsImplementedInterfaces().SingleInstance();
        }

        static IEnumerable<AssemblyName> EnumerateAllAssemblies()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            yield return executingAssembly.GetName();

            foreach (var assembly in executingAssembly.GetReferencedAssemblies())
            {
                yield return assembly;
            }
        }

        public static void RegisterAutomapper(this ContainerBuilder builder)
        {
            var autoMapperProfiles = EnumerateAllAssemblies()
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct()
                .Select(p => Activator.CreateInstance(p) as Profile);

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>()
                .InstancePerLifetimeScope();
        }

        public static IHost InitDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<EventsContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred creating the DB.");
            }

            return host;
        }
    }
}