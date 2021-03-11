using ToDo.Application.Commands;
using ToDo.Domain.DTO;

namespace ToDo.Application.Handlers
{
    public interface ITodoItemCommandHandler
    {
        TodoItemDto Handle(CreateTodoItemCommand command);
        bool Handle(UpdateTodoItemCommand command);
        bool Handle(DeleteTodoItemCommand command);


    }
}
