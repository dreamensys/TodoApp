using System;
using AutoFixture;
using Moq;
using ToDo.Application.Commands;
using ToDo.Application.Handlers;
using ToDo.Domain.Contracts;
using ToDo.Domain.Entities;
using Xunit;

namespace ToDo.Application.Tests
{
    public class TodoItemCommandHandlerTest
    {
        private readonly TodoItemCommandHandler _todoItemsCommandHandler;
        private readonly Mock<IRepository<TodoItem>> _mockTodoRepository;
        private readonly Fixture _fixture;

        public TodoItemCommandHandlerTest()
        {
            _mockTodoRepository = new Mock<IRepository<TodoItem>>();
            _fixture = new Fixture();
            _todoItemsCommandHandler = new TodoItemCommandHandler(_mockTodoRepository.Object);
        }

        [Fact]
        public void Handle_WithNonNullCreationCommand_ShouldReturnNewTodoItem()
        {
            // Arrange
            var command = _fixture.Create<CreateTodoItemCommand>();
            _mockTodoRepository.Setup(s => s.Insert(It.IsAny<TodoItem>()));

            // Act
            var result = _todoItemsCommandHandler.Handle(command);
            
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Handle_WithNullCreationCommand_ShouldThrowException()
        {
            // Arrange
            CreateTodoItemCommand command = null;
            _mockTodoRepository.Setup(s => s.Insert(It.IsAny<TodoItem>()));

            // Act
            Assert.Throws<ArgumentNullException>(() => _todoItemsCommandHandler.Handle(command));
        }

        [Fact]
        public void Handle_WithNonNullUpdatingCommand_ShouldReturnTrue()
        {
            // Arrange
            var command = _fixture.Create<UpdateTodoItemCommand>();
            var todoItem = _fixture.Create<TodoItem>();
            _mockTodoRepository.Setup(s => s.Update(It.IsAny<TodoItem>()));
            _mockTodoRepository.Setup(s => s.Get<TodoItem>(It.IsAny<string>())).Returns(todoItem);

            // Act
            var result = _todoItemsCommandHandler.Handle(command);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Handle_WithNullUpdatingCommand_ShouldThrowException()
        {
            // Arrange
            UpdateTodoItemCommand command = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _todoItemsCommandHandler.Handle(command));
        }

        [Fact]
        public void Handle_WithNonExistentTodoItemId_ShouldTrowException()
        {
            // Arrange
            CreateTodoItemCommand command = null;
            _mockTodoRepository.Setup(s => s.Insert(It.IsAny<TodoItem>()));

            // Assert
            Assert.Throws<ArgumentNullException>(() => _todoItemsCommandHandler.Handle(command));

        }
    }
}
