using TodoApi.Domain;

namespace TodoApi.Application.Todos;

public sealed record GetTodosQuery : IQuery<IReadOnlyList<TodoItem>>;
public sealed record GetTodoByIdQuery(Guid Id) : IQuery<TodoItem?>;

public sealed class GetTodosHandler(ITodoRepository repository)
    : IQueryHandler<GetTodosQuery, IReadOnlyList<TodoItem>>
{
    public async Task<IReadOnlyList<TodoItem>> Handle(GetTodosQuery query, CancellationToken ct) =>
        await repository.GetAllAsync();
}

public sealed class GetTodoByIdHandler(ITodoRepository repository)
    : IQueryHandler<GetTodoByIdQuery, TodoItem?>
{
    public async Task<TodoItem?> Handle(GetTodoByIdQuery query, CancellationToken ct) =>
        await repository.GetAsync(query.Id);
}
