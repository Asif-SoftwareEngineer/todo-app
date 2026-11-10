using System.Collections.Concurrent;
using TodoApi.Application;
using TodoApi.Domain;

namespace TodoApi.Infrastructure;

public sealed class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _items = new();

    public Task<IReadOnlyList<TodoItem>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<TodoItem>>(_items.Values.OrderBy(i => i.CreatedAt).ToList());

    public Task<TodoItem?> GetAsync(Guid id) => Task.FromResult(_items.GetValueOrDefault(id));

    public Task AddAsync(TodoItem item)
    {
        _items[item.Id] = item;
        return Task.CompletedTask;
    }

    public Task<bool> UpdateAsync(TodoItem item) =>
        Task.FromResult(_items.TryGetValue(item.Id, out var existing) && _items.TryUpdate(item.Id, item, existing));

    public Task<bool> RemoveAsync(Guid id) => Task.FromResult(_items.TryRemove(id, out _));
}
