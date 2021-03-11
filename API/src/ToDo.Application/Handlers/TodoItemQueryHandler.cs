using System.Collections.Generic;
using System.Linq;
using ToDo.Application.Queries;
using ToDo.Domain.Contracts;
using ToDo.Domain.DTO;
using ToDo.Domain.Entities;

namespace ToDo.Application.Handlers
{
    public class TodoItemQueryHandler : ITodoItemQueryHandler 
    {
        private readonly IRepository<TodoItem> _todoRepository;

        public TodoItemQueryHandler(IRepository<TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public TodoItemDto Handle(GetTodoItemQuery query)
        {
            if (query == null)
            {
                throw new System.ArgumentNullException("GetTodoItemQuery is required");
            }

            var result = _todoRepository.Get<TodoItem>(query.Id);
            if (result == null)
            {
                return null;
            }

            return new TodoItemDto()
            {
                Id = result.Id,
                Title = result.Title,
                Completed = result.Completed,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            };
        }

        public IEnumerable<TodoItemDto> Handle(GetTodoItemsQuery query)
        {
            if (query == null)
            {
                throw new System.ArgumentNullException("GetTodoItemsQuery is required");
            }

            var results = _todoRepository.GetAll().Select(result => new TodoItemDto()
            {
                Id = result.Id,
                Completed = result.Completed,
                Title = result.Title,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            }).OrderBy(o => o.CreatedAt).ToList();
            return results;
        }
    }
}
