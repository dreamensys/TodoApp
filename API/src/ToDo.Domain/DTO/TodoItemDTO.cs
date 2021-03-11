using System;

namespace ToDo.Domain.DTO
{
    public class TodoItemDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool? Completed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
