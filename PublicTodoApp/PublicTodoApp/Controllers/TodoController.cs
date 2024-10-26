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

    //[HttpPost]
    //public Guid Create(TodoInputDto inputDto)
    //{
    //    var todo = new TodoList
    //    {
    //        Name = inputDto.Name,
    //        Author = inputDto.Author
    //    };
    //    foreach (var taskInputDto in inputDto.Tasks)
    //    {
    //        todo.Tasks.Add(new TodoTask
    //        {
    //            Task = taskInputDto.Task,
    //            CreationDateTime = DateTimeOffset.UtcNow
    //        });
    //    }
    //    todoDbContext.Todos.Add(todo);
    //    todoDbContext.SaveChanges();

    //    return todo.Id;
    //}
}
