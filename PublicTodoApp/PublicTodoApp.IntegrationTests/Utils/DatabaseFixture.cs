
using Microsoft.EntityFrameworkCore;
using PublicTodoApp._DataLayer;
using Testcontainers.MsSql;

namespace PublicTodoApp.IntegrationTests.Utils;

public class DatabaseFixture : IAsyncLifetime
{
    public string TestConnectionString => dbContainer.GetConnectionString();

    private readonly MsSqlContainer dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04")
        .WithName("TEST_TodoAppDb")
        .Build();

    private TodoDbContext? dbContext;

    public async Task InitializeAsync()
    {
        await dbContainer.StartAsync();

        var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>()
            .UseSqlServer(TestConnectionString);
        dbContext = new TodoDbContext(optionsBuilder.Options);
        await dbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        if (dbContext != null)
        {
            await dbContext.DisposeAsync();
        }
        await dbContainer.DisposeAsync();
    }
}

[CollectionDefinition(DatabaseCollection.CollectionDefinitionName)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    public const string CollectionDefinitionName = "IntegrationTests";
}