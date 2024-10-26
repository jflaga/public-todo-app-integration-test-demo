using PublicTodoApp._DomainLayer;

namespace PublicTodoApp.Controllers.Dtos;

public class TodoInputDto
{
    public string Name { get; set; }
    public IEnumerable<TodoTaskInputDto> Tasks { get; set; }
    public string Author { get; set; }
}

