using TodoWebApi.Domain;

namespace TodoApi.Application;

public interface ITodoRepository
{
    Task<IReadOnlyList<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetAsync(Guid id);
    Task AddAsync(TodoItem item);
    Task<bool> UpdateAsync(TodoItem item);
    Task<bool> RemoveAsync(Guid id);
}