using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using CangguEvents.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace CanguuEvents.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EventsContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped<IEventsRepository, SqlLiteRepository>();
            services.AddTransient(typeof(MainWindow));
            RegisterMaps(services);
        }

        private static void RegisterMaps(IServiceCollection services)
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var assembliesTypes = assemblyNames
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile) Activator.CreateInstance(p)).ToList();

            services.AddTransient(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            services.AddTransient(ctx =>
            {
                var mapper = ctx.GetRequiredService<MapperConfiguration>().CreateMapper();
                services.AddScoped(provider => mapper);
                return mapper;
            });
        }
    }
}