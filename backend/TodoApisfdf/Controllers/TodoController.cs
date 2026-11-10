using Microsoft.AspNetCore.Mvc;
using TodoApi.Application;
using TodoApi.Application.Todos;
using TodoApi.Domain;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todos")]
public sealed class TodosController : ControllerBase
{
    // QUERIES (read)

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TodoItem>>> GetAll(
        [FromServices] IQueryHandler<GetTodosQuery, IReadOnlyList<TodoItem>> handler,
        CancellationToken ct) =>
        Ok(await handler.Handle(new GetTodosQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TodoItem>> GetById(
        Guid id,
        [FromServices] IQueryHandler<GetTodoByIdQuery, TodoItem?> handler,
        CancellationToken ct)
    {
        var item = await handler.Handle(new GetTodoByIdQuery(id), ct);
        return item is null ? NotFound() : Ok(item);
    }

    // COMMANDS (write)

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Add(
        CreateTodoRequest request,
        [FromServices] ICommandHandler<AddTodoCommand, TodoItem> handler,
        CancellationToken ct)
    {
        var item = await handler.Handle(new AddTodoCommand(request.Title), ct);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TodoItem>> Update(
        Guid id,
        UpdateTodoRequest request,
        [FromServices] ICommandHandler<UpdateTodoCommand, TodoItem?> handler,
        CancellationToken ct)
    {
        var item = await handler.Handle(new UpdateTodoCommand(id, request.Title, request.IsCompleted), ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] ICommandHandler<DeleteTodoCommand, bool> handler,
        CancellationToken ct) =>
        await handler.Handle(new DeleteTodoCommand(id), ct) ? NoContent() : NotFound();
}