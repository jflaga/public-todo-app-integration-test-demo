using Azure.Core;
using Microsoft.OpenApi.Models;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.IntegrationTests.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PublicTodoApp.IntegrationTests.Resources;

[Collection(DatabaseCollection.CollectionDefinitionName)]
public class CreateTodoTaskTests : IClassFixture<PublicTodoAppWebApplicationFactory>
{
    private static readonly JsonSerializerOptions JsonWebOptions = new(JsonSerializerDefaults.Web);
    private readonly PublicTodoAppWebApplicationFactory webAppFactory;

    public CreateTodoTaskTests(PublicTodoAppWebApplicationFactory webAppFactory)
    {
        this.webAppFactory = webAppFactory;
    }

    [Fact]
    public async Task CreateTodoTask_HappyPath_Succeeds()
    {
        var createTodoPayload = new TodoList
        {
            Id = Guid.NewGuid(),
            Name = "My Todo List",
            Author = "Jboy",
            Tasks =
            [
                new TodoTask {
                    Id = Guid.NewGuid(),
                    Task = "Read Mere Christianity"
                },
                new TodoTask {
                    Id = Guid.NewGuid(),
                    Task = "Read Learning DDD"
                },
            ]
        };

        await CallCreateTodoApiEndpoint(createTodoPayload);

        // Call create todo task endpoint
        var createTodoTaskPayload = new TodoTask
        {
            Id = Guid.NewGuid(),
            Task = "Read Master Software Architecture",
            TodoListId = createTodoPayload.Id,
        };
        var client = webAppFactory.CreateClient();
        var response = await client.PostAsJsonAsync($"/api/todotasks/{createTodoPayload.Id}", createTodoTaskPayload);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);

        await AssertThatTodoAndTasksWasSaved(createTodoPayload.Id);
    }

    private async Task CallCreateTodoApiEndpoint(TodoList payload)
    {
        var client = webAppFactory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/todos", payload);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);
    }

    private async Task AssertThatTodoAndTasksWasSaved(Guid todoListId)
    {
        var client = webAppFactory.CreateClient();
        var response = await client.GetAsync($"/api/todos/{todoListId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var actualTodo = JsonSerializer.Deserialize<TodoList>(responseContent, JsonWebOptions);
        Assert.NotNull(actualTodo);
        Assert.Equal("My Todo List", actualTodo.Name);
        Assert.Equal("Jboy", actualTodo.Author);

        Assert.Equal(3, actualTodo.Tasks.Count);

        Assert.Contains(actualTodo.Tasks, x => x.Task == "Read Mere Christianity");
        Assert.Contains(actualTodo.Tasks, x => x.Task == "Read Learning DDD");
        Assert.Contains(actualTodo.Tasks, x => x.Task == "Read Master Software Architecture");
    }
}
