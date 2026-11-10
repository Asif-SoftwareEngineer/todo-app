
using TodoApi.Application;
using TodoWebApi.Domain;


namespace TodoWebApi.Application.Todos;

public sealed record AddTodoCommand(string Title) : ICommand<TodoItem>;
public sealed record UpdateTodoCommand(Guid Id, string Title, bool IsCompleted) : ICommand<TodoItem?>;
public sealed record DeleteTodoCommand(Guid Id) : ICommand<bool>;

public sealed class AddTodoHandler(ITodoRepository repository, TimeProvider clock)
    : ICommandHandler<AddTodoCommand, TodoItem>
{
    public async Task<TodoItem> Handle(AddTodoCommand command, CancellationToken ct)
    {
        var item = new TodoItem(Guid.NewGuid(), command.Title.Trim(), false, clock.GetUtcNow());
        await repository.AddAsync(item);
        return item;
    }
}

public sealed class UpdateTodoHandler(ITodoRepository repository)
    : ICommandHandler<UpdateTodoCommand, TodoItem?>
{
    public async Task<TodoItem?> Handle(UpdateTodoCommand command, CancellationToken ct)
    {
        var existing = await repository.GetAsync(command.Id);
        if (existing is null) return null;

        var updated = existing with { Title = command.Title.Trim(), IsCompleted = command.IsCompleted };
        return await repository.UpdateAsync(updated) ? updated : null;
    }
}

public sealed class DeleteTodoHandler(ITodoRepository repository)
    : ICommandHandler<DeleteTodoCommand, bool>
{
    public async Task<bool> Handle(DeleteTodoCommand command, CancellationToken ct) =>
        await repository.RemoveAsync(command.Id);
}
