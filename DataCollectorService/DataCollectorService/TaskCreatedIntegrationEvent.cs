namespace PublicTodoApp.Etos;

public class TaskCreatedIntegrationEvent
{
    public string Task { get; set; }
    public DateTimeOffset CreationDateTime { get; set; }
}
