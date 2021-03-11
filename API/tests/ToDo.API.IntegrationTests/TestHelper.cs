using System;
using System.Collections.Generic;
using System.Text;
using ToDo.API.Models;

namespace ToDo.API.IntegrationTests
{
    public class TestHelper
    {
        public NewTodoItem GetNewTodoItemDto()
        {
            return new NewTodoItem() { Title = "Task 1" };
        }

}
}
