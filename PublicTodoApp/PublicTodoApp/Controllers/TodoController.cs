using Microsoft.AspNetCore.Mvc;
using PublicTodoApp._DataLayer;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.Controllers.Dtos;

namespace PublicTodoApp.Controllers;
[ApiController]
[Route("[controller]")]
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

    [HttpPost]
    public void Create(TodoList todo)
    {
        todoDbContext.Todos.Add(todo);
        todoDbContext.SaveChanges();
    }
}
