using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Moq;
using ToDo.Application.Handlers;
using ToDo.Application.Queries;
using ToDo.Domain.Contracts;
using ToDo.Domain.Entities;
using Xunit;

namespace ToDo.Application.Tests
{
    public class TodoItemQueryHandlerTest
    {
        private readonly ITodoItemQueryHandler _todoItemsQueryHandler;
        private readonly Mock<IRepository<TodoItem>> _mockTodoRepository;
        private readonly Fixture _fixture;

        public TodoItemQueryHandlerTest()
        {
            // Mocking Methods
            _mockTodoRepository = new Mock<IRepository<TodoItem>>();
            _fixture = new Fixture();
            _todoItemsQueryHandler = new TodoItemQueryHandler(_mockTodoRepository.Object);
        }

        [Fact]
        public void Handle_WithNonNullQuery_ShouldReturnListOfItems()
        {
            // Arrange
            GetTodoItemsQuery query = new GetTodoItemsQuery();
            var expected = new List<TodoItem>()
            {
                _fixture.Create<TodoItem>(),
                _fixture.Create<TodoItem>(),
                _fixture.Create<TodoItem>()
            };
            _mockTodoRepository.Setup(s => s.GetAll()).Returns(expected);

            // Act
            var result = _todoItemsQueryHandler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Count, result.Count());
        }

        [Fact]
        public void Handle_WithNullGetTodoItemsQuery_ShouldTrowException()
        {
            // Arrange
            GetTodoItemsQuery query = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _todoItemsQueryHandler.Handle(query));
        }

        [Fact]
        public void Handle_WithNonNullQuery_ShouldReturnTodoItem()
        {
            // Arrange
            TodoItem expected = _fixture.Create<TodoItem>();
            GetTodoItemQuery query = new GetTodoItemQuery() { Id = expected.Id };
            _mockTodoRepository.Setup(s => s.Get<TodoItem>(It.IsAny<string>())).Returns(expected);

            // Act
            var result = _todoItemsQueryHandler.Handle(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Title, result.Title);
            Assert.Equal(expected.Completed, result.Completed);
        }

        [Fact]
        public void Handle_WithNullGetTodoItemQuery_ShouldTrowException()
        {
            // Arrange
            GetTodoItemQuery query = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _todoItemsQueryHandler.Handle(query));
        }
    }
}
