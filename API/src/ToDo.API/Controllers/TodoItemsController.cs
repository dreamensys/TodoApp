using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using ToDo.API.Models;
using ToDo.Application.Commands;
using ToDo.Application.Handlers;
using ToDo.Application.Queries;
using ToDo.Domain.DTO;

namespace ToDo.API.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemQueryHandler _todoItemsQueryHandler;
        private readonly ITodoItemCommandHandler _todoItemsCommandHandler;
        private const string NotFoundMessage = "Not found, no record found for your input criteria.";

        private const string BadRequestMessage =
            "Bad request, invalid input information is supplied or there is inconsistency in the requested data.";

        private const string NoContentMessage =
            "The server successfully processed the request and is not returning any content.";

        public TodoItemsController(ITodoItemQueryHandler getTodoItemsQueryHandler, ITodoItemCommandHandler todoItemsCommandHandler)
        {
            _todoItemsQueryHandler = getTodoItemsQueryHandler;
            _todoItemsCommandHandler = todoItemsCommandHandler;
        }

        /// <summary>
        /// Returns all the tasks.
        /// </summary>
        /// <remarks>
        /// Returns all the tasks.
        /// </remarks>
        /// <returns>A list of tasks.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDto>> GetAll()
        {
            var items = _todoItemsQueryHandler.Handle(new GetTodoItemsQuery());
            return Ok(items);
        }

        /// <summary>
        /// Returns all the info of a task.
        /// </summary>
        /// <remarks>
        /// Returns all the info of a task.
        /// </remarks>
        /// <param name="todoItemId">The unique identifier of the task.</param>
        /// <returns>The task retrieved.</returns>
        [HttpGet]
        [Route("{todoItemId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TodoItemDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, NotFoundMessage)]
        public IActionResult Get(string todoItemId)
        {
            var item = _todoItemsQueryHandler.Handle(new GetTodoItemQuery() { Id = todoItemId });
            if (item == null)
            {
                return NotFound(new { Message = $"Item with id {todoItemId} not found." });
            }
            return Ok(item);
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <remarks>
        /// Creates a new task.
        /// </remarks>
        /// <param name="request">Payload with the properties to be updated in the todo item.</param>
        /// <returns>The new task created.</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TodoItemDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, NotFoundMessage)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, BadRequestMessage)]
        public IActionResult Post(NewTodoItem request)
        {
            if (request == null || request.Title == string.Empty)
            {
                return BadRequest("All parameters are required.");
            }

            var item = _todoItemsCommandHandler.Handle(new CreateTodoItemCommand()
            {
                Title = request.Title,
            });
            
            return Ok(item);
        }

        /// <summary>
        /// Updates the todo item information.
        /// </summary>
        /// <remarks>
        /// Updates the todo item information.
        /// </remarks>
        /// <param name="todoItemId">Unique identifier of the Todo Item.</param>
        /// <param name="request">Payload with the properties to be updated in the todo item.</param>
        /// <returns>No Content</returns>
        [HttpPut]
        [Route("{todoItemId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, NoContentMessage)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, NotFoundMessage)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, BadRequestMessage)]
        public IActionResult Put(string todoItemId, [FromBody] UpdateTodoitem request)
        {
            if (request == null || request.Title == string.Empty)
            {
                return BadRequest("All parameters are required.");
            }

            var item = _todoItemsQueryHandler.Handle(new GetTodoItemQuery() { Id = todoItemId });
            if (item == null)
            {
                return NotFound(new { Message = $"Item with id {todoItemId} not found." });
            }

            _todoItemsCommandHandler.Handle(new UpdateTodoItemCommand()
            {
                Id = todoItemId,
                Title = request.Title,
                Completed = request.Completed
            });

            return NoContent();
        }

        /// <summary>
        /// Delete a todo item.
        /// </summary>
        /// <remarks>
        /// Delete a todo item.
        /// </remarks>
        /// <param name="todoItemId">Unique identifier of the Todo Item.</param>
        /// <returns>No Content</returns>
        [HttpDelete("{todoItemId}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, NoContentMessage)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, NotFoundMessage)]
        public IActionResult Delete(string todoItemId)
        {
            var item = _todoItemsQueryHandler.Handle(new GetTodoItemQuery() { Id = todoItemId });
            if (item == null)
            {
                return NotFound(new { Message = $"Item with id {todoItemId} not found." });
            }

            _todoItemsCommandHandler.Handle(new DeleteTodoItemCommand() { Id = todoItemId });

            return NoContent();
        }
    }
}
