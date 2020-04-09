using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CangguEvents.SQLite.Entities
{
    public class EventEntity : StorageItem
    {
        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LocationEntity Location { get; set; }

        public int LocationId { get; set; }

        public virtual ICollection<DayOfWeekEntity> DayOfWeeks { get; set; } = new List<DayOfWeekEntity>();
    }
}