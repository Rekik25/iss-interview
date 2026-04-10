# Solution Documentation

**Candidate Name:** Rohit Kumar 
**Completion Date:** 11th April 2026

---

## Problems Identified

_Describe the issues you found in the original implementation. Consider aspects like:_
### Architecture and Design Patterns
- The codebase lacked a well-defined layered structure.
- Controllers performed data access directly, creating tight coupling with persistence logic.
- There was no reliable database initialization step, resulting in runtime failures.

### Code Quality and Maintainability
- Connection strings were hardcoded, making environment-specific configuration difficult.
- Missing interface abstractions reduced modularity and made unit testing harder.
- Inconsistent naming across controllers and services reduced readability and increased confusion.

### Security Vulnerabilities
- API inputs were not validated, exposing the application to malformed or malicious data.
- No centralized error handling or exception filtering to standardize responses and logging.
- Authentication and authorization were not implemented, leaving endpoints unprotected.

### Performance Concerns
- Synchronous database operations risk blocking threads under high load.
- Endpoints lacked pagination and filtering, which hurts scalability for large TODO datasets.

### Testing Gaps
- The supplied test templates did not match actual constructors and method signatures.
- The repository layer was difficult to mock, preventing isolated tests for services.
- Test coverage lacked meaningful positive and negative scenarios.

---

## Architectural Decisions

_Explain the architecture you chose and why. Consider:_
### Design Patterns Applied
- Adopted a layered architecture (Controller → Service → Repository) to separate responsibilities.
- Implemented the Repository pattern to encapsulate data access and simplify testing.
- Used dependency injection via the ASP.NET Core DI container for decoupling and easier composition.

### Project Structure Changes
- Organized the codebase into clear folders for Controllers, Services, Repositories, Models, DTOs, and Data to improve navigation and responsibility boundaries.
- Added a `DbInitializer` to ensure the database schema is created or updated at application startup.

### Technology Choices
- Chose ASP.NET Core Web API to provide RESTful endpoints.
- Selected SQLite as a lightweight, file-based persistence option for simplicity and portability.
- Used ADO.NET for explicit and straightforward database interactions.

### Separation of Concerns
- Controllers are responsible only for handling HTTP requests and responses.
- Services encapsulate business rules, validation, and coordination logic.
- Repositories manage all interactions with the data store, keeping persistence details isolated.
---

## Trade-offs

_Discuss compromises you made and the reasoning behind them. Consider:_
### What Did I Prioritize?
- Simple and explicit architecture over heavy abstractions.
- Clarity, correctness, and maintainability
- Predictable database behavior.

### What Did I Defer or Simplify?
- Authentication and authorization were excluded.
- Asynchronous database operations were not implemented.
- Advanced logging and monitoring were omitted.

### What Alternatives Did I Consider?
- Entity Framework Core
- In-memory database (rejected due to persistence requirements).
---

## How to Run

### Prerequisites
- .NET SDK 7.0 or later
- Windows / Linux / macOS
- PowerShell or terminal
- Git (optional, for repository cloning)

### Build
```bash
```bash
dotnet clean
dotnet build
```

### Run
```bash
dotnet run --project TodoApi
```

### Test
```bash
dotnet test
```

---

## API Documentation

### Endpoints

#### Create TODO
```
Method: POST
URL: /api/todos
Request Body:
{
  "title": "Do homework",
  "description": "Of Science"
}

Response:
{
  "message": "Todo created successfully"
}

```

#### Get TODO(s)
```
Method: GET
URL: /api/todos

Response:
[
  {
    "id": 1,
    "title": "Do homework",
    "description": "Of Maths",
    "isCompleted": false,
    "createdAt": "2026-03-11T16:30:00Z"
  }
]
```

#### Update TODO
```
Method: PUT
URL: /api/todos/{id}

Request Body:
{
  "title": "Make Curd",
  "description": "Store it in fridge later",
  "isCompleted": true
}

Response
{
  "message": "Todo updated successfully"
}
```

#### Delete TODO
```
Method: DELETE
URL: /api/todos/{id}

Response:
{
  "message": "Todo deleted successfully"
}
```

---

## Future Improvements

_What would you do if you had more time? Consider:_
- Adopt Entity Framework Core with migrations to provide structured schema management and versioning.
- Convert database interactions to asynchronous operations to improve responsiveness and scalability.
- Broaden unit and integration test suites to cover both successful and failure scenarios.
- Add global exception-handling middleware to standardize error responses and centralized logging.
- Implement authentication and authorization to secure API endpoints and enforce access control.
- Introduce pagination, filtering, and sorting on list endpoints to efficiently handle large datasets.
- Containerize the application with Docker for consistent, reproducible deployments.