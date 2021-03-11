using System;

namespace ToDo.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Title { get; set; }
        public bool? Completed { get; set; }

        public int? Order { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
