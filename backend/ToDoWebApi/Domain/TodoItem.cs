namespace TodoWebApi.Domain;

public sealed record TodoItem(Guid Id, string Title, bool IsCompleted, DateTimeOffset CreatedAt);
