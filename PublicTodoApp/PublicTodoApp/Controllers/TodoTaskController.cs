using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicTodoApp._DataLayer;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.Controllers.Dtos;
using PublicTodoApp.Etos;

namespace PublicTodoApp.Controllers;

[ApiController]
[Route("api/todotasks")]
public class TodoTaskController : ControllerBase
{
    private readonly TodoDbContext todoDbContext;
    private readonly IPublishEndpoint publishEndpoint;

    public TodoTaskController(TodoDbContext todoDbContext, IPublishEndpoint publishEndpoint)
    {
        this.todoDbContext = todoDbContext;
        this.publishEndpoint = publishEndpoint;
    }

    [HttpPost("{todoId}")]
    public async Task Create([FromRoute] Guid todoId, TodoTask task)
    {
        try
        {
            todoDbContext.TodoTasks.Add(task);
            todoDbContext.SaveChanges();

            await publishEndpoint.Publish(new TaskCreatedIntegrationEvent
            {
                Task = task.Task,
                CreationDateTime = task.CreationDateTime
            });
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
