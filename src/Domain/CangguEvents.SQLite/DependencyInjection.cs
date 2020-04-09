using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CangguEvents.SQLite
{
    public static class DependencyInjection
    {
        public static void AddSqlLite(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<SqlLiteRepository>();
            services.AddDbContext<EventsContext>(options => options.UseSqlite(
                configuration.GetConnectionString("EventDb")));
        }
    }
}