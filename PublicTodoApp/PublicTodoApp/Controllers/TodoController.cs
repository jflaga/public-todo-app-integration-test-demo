using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicTodoApp._DataLayer;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.Controllers.Dtos;

namespace PublicTodoApp.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext todoDbContext;

    public TodoController(TodoDbContext todoDbContext)
    {
        this.todoDbContext = todoDbContext;
    }

    [HttpGet]
    public IEnumerable<TodoList> Get()
    {
        return todoDbContext.Todos;
    }

    [HttpGet("{todoId}")]
    public TodoList Get([FromRoute] Guid todoId)
    {
        return todoDbContext.Todos.Include(x => x.Tasks).SingleOrDefault(x => x.Id == todoId);
    }

    [HttpPost]
    public void Create(TodoList todo)
    {
        todoDbContext.Todos.Add(todo);
        todoDbContext.SaveChanges();
    }
}
