using System;
using ToDo.Application.Commands;
using ToDo.Domain.Contracts;
using ToDo.Domain.DTO;
using ToDo.Domain.Entities;

namespace ToDo.Application.Handlers
{
    public class TodoItemCommandHandler : ITodoItemCommandHandler 
    {
        private readonly IRepository<TodoItem> _todoRepository;

        public TodoItemCommandHandler(IRepository<TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public TodoItemDto Handle(CreateTodoItemCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("CreateTodoItemCommand can not be null");
            }

            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid().ToString(),
                Title = command.Title,
                CreatedAt = DateTime.Now,
                Completed = false
            };
            _todoRepository.Insert(todoItem);

            var todoItemDto = new TodoItemDto()
            {
                Id = todoItem.Id,
                Completed = todoItem.Completed,
                Title = todoItem.Title,
                CreatedAt = todoItem.CreatedAt
            };

            return todoItemDto;
        }

        public bool Handle(UpdateTodoItemCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("UpdateTodoItemCommand can not be null");
            }

            var currentItem = _todoRepository.Get<TodoItem>(command.Id);
            if (currentItem == null)
            {
                throw new NullReferenceException("Not found");
            }
            currentItem.Completed = command.Completed;
            currentItem.Title = command.Title;
            currentItem.UpdatedAt = DateTime.Now;
            try
            {
                _todoRepository.Update(currentItem);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Handle(DeleteTodoItemCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("UpdateTodoItemCommand can not be null");
            }

            try
            {
                _todoRepository.Delete(command.Id);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
