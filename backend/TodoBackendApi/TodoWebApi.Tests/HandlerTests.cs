using TodoWebApi.Application.Todos;
using TodoWebApi.Infrastructure;

namespace TodoWebApi.Tests;

public class HandlerTests
{
    private readonly InMemoryTodoRepository _repo = new();

    [Fact]
    public async Task Add_then_query_returns_trimmed_incomplete_item()
    {
        var added = await new AddTodoHandler(_repo, TimeProvider.System).Handle(new AddTodoCommand("  Buy milk "), default);
        var all = await new GetTodosHandler(_repo).Handle(new GetTodosQuery(), default);

        Assert.Equal("Buy milk", added.Title);
        Assert.False(added.IsCompleted);
        Assert.Single(all);
    }

    [Fact]
    public async Task Update_changes_item_and_returns_null_when_missing()
    {
        var added = await new AddTodoHandler(_repo, TimeProvider.System).Handle(new AddTodoCommand("A"), default);
        var handler = new UpdateTodoHandler(_repo);

        var updated = await handler.Handle(new UpdateTodoCommand(added.Id, "B", true), default);
        var missing = await handler.Handle(new UpdateTodoCommand(Guid.NewGuid(), "X", true), default);

        Assert.Equal(("B", true), (updated!.Title, updated.IsCompleted));
        Assert.Null(missing);
    }

    [Fact]
    public async Task Delete_removes_item_once()
    {
        var added = await new AddTodoHandler(_repo, TimeProvider.System).Handle(new AddTodoCommand("A"), default);
        var handler = new DeleteTodoHandler(_repo);

        Assert.True(await handler.Handle(new DeleteTodoCommand(added.Id), default));
        Assert.False(await handler.Handle(new DeleteTodoCommand(added.Id), default));
    }


}
