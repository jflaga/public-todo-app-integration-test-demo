namespace PublicTodoApp._DomainLayer;

public class TodoList
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<TodoTask> Tasks { get; set; }
    public string Author { get; set; }
}
