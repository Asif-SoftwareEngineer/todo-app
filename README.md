# Todo List: Angular 22 + .NET 10

A simple todo app: view, add, complete and delete tasks.

## Tech

- Backend: .NET 10 Web API, controllers, CQRS (commands/queries + handlers), in-memory repository, Swagger
- Frontend: Angular 22, Angular Material, reactive forms, signals
- Tests: xUnit (unit + integration) - Program modules loads and run in Test Project (for integration tests)

## Run

Requires .NET 10 SDK and Node 22.22+ / 24.15+ / 26+.

Backend (https://localhost:7080/swagger):
cd backend
dotnet dev-certs https --trust
dotnet run --project TodoApi --launch-profile https

Frontend (http://localhost:4200):
cd frontend
npm install
npm start

## Test

    cd backend && dotnet test

## Structure

- backend/TodoApi/Application: CQRS commands, queries, handlers, repository interface
- backend/TodoApi/Infrastructure: in-memory repository
- backend/TodoApi/Controllers: API layer
- frontend/src/app: component, service, model
