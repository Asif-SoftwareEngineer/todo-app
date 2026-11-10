using TodoApi.Application;
using TodoWebApi.Application.Todos;
using TodoWebApi.Domain;
using TodoWebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddOpenApi(options =>
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "Todo Backend API";
        document.Info.Description = "A simple todo list API built with CQRS.";
        document.Info.Version = "1.0";
        document.Info.Contact = new() { Name = "Asif Javed", Email = "asif.javed@example.com" };
        return Task.CompletedTask;
    }));


builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

builder.Services.AddScoped<IQueryHandler<GetTodosQuery, IReadOnlyList<TodoItem>>, GetTodosHandler>();
builder.Services.AddScoped<IQueryHandler<GetTodoByIdQuery, TodoItem?>, GetTodoByIdHandler>();
builder.Services.AddScoped<ICommandHandler<AddTodoCommand, TodoItem>, AddTodoHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateTodoCommand, TodoItem?>, UpdateTodoHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteTodoCommand, bool>, DeleteTodoHandler>();

var app = builder.Build();
app.UseHttpsRedirection();   // must come first, so it sees every request
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "Todo API v1"));  // serves /swagger
}


app.MapControllers();
app.Run();

public partial class Program; // exposed for integration tests
