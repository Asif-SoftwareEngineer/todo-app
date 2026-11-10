using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Controllers;

public sealed record CreateTodoRequest(
    [Required, MaxLength(200)] string Title);

public sealed record UpdateTodoRequest(
    [Required, MaxLength(200)] string Title,
    bool IsCompleted);