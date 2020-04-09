using System;
using System.ComponentModel.DataAnnotations;

namespace CangguEvents.SQLite.Entities
{
    public class DayOfWeekEntity : StorageItem
    {
        public int EventEntityId { get; set; }
        public virtual EventEntity EventEntity { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}