# Todo List: Angular 22 + .NET 10

A simple todo app: view, add, complete and delete tasks.

## Tech

    - Backend: .NET 10 Web API, controllers, CQRS (commands/queries + handlers), in-memory repository, Swagger
    - Frontend: Angular 22, Angular Material, reactive forms, signals
    - Tests: xUnit (unit + integration) - Program modules loads and run in Test Project (for integration tests)

## Run

    Requires .NET 10 SDK and Node 22.22+ / 24.15+ / 26+.

Backend (https://localhost:7080/swagger):

        Following commands can help run the backend prroject:

        1- cd backend
        2- dotnet dev-certs https --trust
        3- dotnet run --project TodoApi --launch-profile https

        OR Solution can be run using visual studio by running it with https profile, that will trigger the swagger UI and backend running

Frontend (http://localhost:4200):

        Following commands can help run the backend prroject:

        1- cd frontend
        2- npm install
        3- npm start

        OR front end solution can be opened using visual studio code, the command mentioned obove can be run inside the visual studio code terminal window, which will help run the frontend on http://localhost:4200

## Test

        1- cd backend
        2- dotnet test

## Structure

- backend/TodoApi/Application: CQRS commands, queries, handlers, repository interface
- backend/TodoApi/Infrastructure: in-memory repository
- backend/TodoApi/Controllers: API layer
- frontend/src/app: component, service, model
