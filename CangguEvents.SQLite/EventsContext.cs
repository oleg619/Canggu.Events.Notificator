using CangguEvents.SQLite.Entities;
using Microsoft.EntityFrameworkCore;

namespace CangguEvents.SQLite
{
    public class EventsContext : DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
        }

        public DbSet<EventEntity> Events { get; set; }
        public DbSet<UserStateEntity> Users { get; set; }
    }
}