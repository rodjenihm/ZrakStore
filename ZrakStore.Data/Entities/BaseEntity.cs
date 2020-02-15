using System;

namespace ZrakStore.Data.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
