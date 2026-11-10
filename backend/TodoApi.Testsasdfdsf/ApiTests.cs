using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoApi.Domain;

namespace TodoApi.Tests;

public class ApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Full_lifecycle_add_update_delete()
    {
        var created = await _client.PostAsJsonAsync("/api/todos", new { title = "Write tests" });
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        var item = (await created.Content.ReadFromJsonAsync<TodoItem>())!;

        var put = await _client.PutAsJsonAsync($"/api/todos/{item.Id}", new { title = "Write tests", isCompleted = true });
        Assert.True((await put.Content.ReadFromJsonAsync<TodoItem>())!.IsCompleted);

        Assert.Equal(HttpStatusCode.NoContent, (await _client.DeleteAsync($"/api/todos/{item.Id}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await _client.DeleteAsync($"/api/todos/{item.Id}")).StatusCode);
    }

    [Fact]
    public async Task Post_rejects_title_longer_than_200_characters()
    {
        var response = await _client.PostAsJsonAsync("/api/todos", new { title = new string('a', 201) });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Post_rejects_blank_title(string title)
    {
        var response = await _client.PostAsJsonAsync("/api/todos", new { title });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
