using Microsoft.EntityFrameworkCore;
using PublicTodoApp._DomainLayer;

namespace PublicTodoApp._DataLayer;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoList> Todos { get; set; }
}
