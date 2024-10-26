using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicTodoApp._DataLayer;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.Controllers.Dtos;

namespace PublicTodoApp.Controllers;

[ApiController]
[Route("api/todotasks")]
public class TodoTaskController : ControllerBase
{
    private readonly TodoDbContext todoDbContext;

    public TodoTaskController(TodoDbContext todoDbContext)
    {
        this.todoDbContext = todoDbContext;
    }

    [HttpPost("{todoId}")]
    public void Create([FromRoute] Guid todoId, TodoTask task)
    {
        todoDbContext.TodoTasks.Add(task);
        todoDbContext.SaveChanges();
    }
}
