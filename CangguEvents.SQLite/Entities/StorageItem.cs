using System;

namespace CangguEvents.SQLite.Entities
{
    public abstract class StorageItem
    {
        public DateTime CreationTime { get; set; }

        public int Id { get; set; }

        public StorageItem()
        {
            CreationTime = DateTime.UtcNow;
        }
    }
}