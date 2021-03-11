namespace ToDo.Application.Commands
{
    public class UpdateTodoItemCommand
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
