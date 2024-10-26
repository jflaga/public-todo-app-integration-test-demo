﻿using Azure.Core;
using Microsoft.OpenApi.Models;
using PublicTodoApp._DomainLayer;
using PublicTodoApp.Controllers.Dtos;
using PublicTodoApp.IntegrationTests.Utils;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PublicTodoApp.IntegrationTests.Resources;

[Collection(DatabaseCollection.CollectionDefinitionName)]
public class CreateTodoTests : IClassFixture<PublicTodoAppWebApplicationFactory>
{
    private static readonly JsonSerializerOptions JsonWebOptions = new(JsonSerializerDefaults.Web);
    private readonly PublicTodoAppWebApplicationFactory webAppFactory;

    public CreateTodoTests(PublicTodoAppWebApplicationFactory webAppFactory)
    {
        this.webAppFactory = webAppFactory;
    }

    [Fact]
    public async Task CreateTodo_HappyPath_Succeeds()
    {
        var payload = new TodoList
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
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
        var client = webAppFactory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/todos", payload);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);
        await AssertThatTodoAndTasksWasSaved(payload);
    }

    private async Task AssertThatTodoAndTasksWasSaved(TodoList expectedTodo)
    {
        var client = webAppFactory.CreateClient();
        var response = await client.GetAsync($"/api/todos/{expectedTodo.Id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        var actualTodo = JsonSerializer.Deserialize<TodoList>(responseContent, JsonWebOptions);
        Assert.NotNull(actualTodo);
        Assert.Equal(expectedTodo.Name, actualTodo.Name);
        Assert.Equal(expectedTodo.Author, actualTodo.Author);

        Assert.Equal(expectedTodo.Tasks.Count, actualTodo.Tasks.Count);

        foreach (var actualTask in actualTodo.Tasks)
        {
            var expectedTask = actualTodo.Tasks.FirstOrDefault(x => x.Id == actualTask.Id);
            Assert.Equal(expectedTask.Task, actualTask.Task);
        }
    }

    //[Fact]
    //public async Task CreateTodo_HappyPath_Succeeds()
    //{
    //    var payload = new TodoInputDto
    //    {
    //        Name = "My Todo List",
    //        Author = "Jboy",
    //        Tasks =
    //        [
    //            new TodoTaskInputDto {
    //                Task = "Read Mere Christianity"
    //            },
    //            new TodoTaskInputDto {
    //                Task = "Read Learning DDD"
    //            },
    //        ]
    //    };
    //    var client = webAppFactory.CreateClient();

    //    var response = await client.PostAsJsonAsync("/api/todos", payload);
    //    var responseContent = await response.Content.ReadAsStringAsync();
    //    var todoId = Guid.Parse(responseContent);
    //    Assert.True(response.IsSuccessStatusCode);
    //    await AssertThatTodoAndTasksWasSaved(payload, todoId);
    //}

    //private async Task AssertThatTodoAndTasksWasSaved(TodoInputDto expectedTodo, Guid todoId)
    //{
    //    var client = webAppFactory.CreateClient();
    //    var response = await client.GetAsync($"/api/todos/{todoId}");
    //    var responseContent = await response.Content.ReadAsStringAsync();
    //    var actualTodo = JsonSerializer.Deserialize<TodoList>(responseContent, JsonWebOptions);
    //    Assert.NotNull(actualTodo);
    //    Assert.Equal(expectedTodo.Name, actualTodo.Name);
    //    Assert.Equal(expectedTodo.Author, actualTodo.Author);

    //    Assert.Equal(expectedTodo.Tasks.Count(), actualTodo.Tasks.Count);

    //    foreach (var actualTask in actualTodo.Tasks)
    //    {
    //        var expectedTask = actualTodo.Tasks.FirstOrDefault(x => x.Id == actualTask.Id);
    //        Assert.Equal(expectedTask.Task, actualTask.Task);
    //    }
    //}
}
