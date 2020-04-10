using AutoMapper;
using CangguEvents.Domain;
using CangguEvents.MongoDb.Initialization;
using CangguEvents.MongoDb.Options;
using CangguEvents.MongoDb.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CangguEvents.MongoDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMongo();
            services.AddInitializers(typeof(IMongoDbInitializer));

            return services;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }

        public static void AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(context =>
            {
                var configuration = context.GetService<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>("Mongo");

                return options;
            });

            services.AddSingleton(context =>
            {
                var options = context.GetService<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            });

            services.AddScoped(context =>
            {
                var options = context.GetService<MongoDbOptions>();
                var client = context.GetService<MongoClient>();
                return client.GetDatabase(options.Database);
            });

            services.AddScoped<IMongoDbInitializer, MongoDbInitializer>();

            services.AddScoped<IUserStateRepository>(ctx =>
                new UserStatesMongoRepository(ctx.GetService<IMongoDatabase>(), "user_states"));
            services.AddScoped<IEventsRepository>(ctx =>
                new EventsMongoDbRepository(ctx.GetService<IMongoDatabase>(), ctx.GetService<IMapper>(), "events"));
        }
    }
}