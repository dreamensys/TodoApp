using System.Collections.Generic;
using ToDo.Application.Queries;
using ToDo.Domain.DTO;

namespace ToDo.Application.Handlers
{
    public interface ITodoItemQueryHandler
    {
        TodoItemDto Handle(GetTodoItemQuery query);
        IEnumerable<TodoItemDto> Handle(GetTodoItemsQuery query);

    }
}
