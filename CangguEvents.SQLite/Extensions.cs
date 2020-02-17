using System.Linq;
using CangguEvents.SQLite.Entities;
using Microsoft.EntityFrameworkCore;

namespace CangguEvents.SQLite
{
    public static class Extensions
    {
        public static IQueryable<EventEntity> IncludeAllInfo(this IQueryable<EventEntity> queryable)
        {
            return queryable
                .Include(entity => entity.Location)
                .Include(entity => entity.DayOfWeeks);
        }
    }
}