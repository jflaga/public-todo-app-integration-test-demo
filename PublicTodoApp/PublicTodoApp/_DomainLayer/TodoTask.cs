namespace PublicTodoApp._DomainLayer;

public class TodoTask
{
    public Guid Id { get; set; }
    public string Task { get; set; }
    public DateTimeOffset CreationDateTime { get; set; }
    public DateTimeOffset CompletionDateTime { get; set; }

    public Guid TodoListId { get; set; }
}