# TaskManager Solution
## Overview

TaskManager is a full-stack solution for managing tasks, users, and workflows. It supports multiple task types, status transitions, and user assignments. The solution is built with clean architecture principles and includes:

- **TaskManager.APIServer**: ASP.NET Core Web API backend.
- **TaskManager.MVC**: ASP.NET Core MVC (Razor Pages) frontend.
- **TaskManager.React**: React + Vite + TypeScript SPA frontend (work in progress).
- **TaskManager.Logic**: Shared business logic, DTOs, and service interfaces.
- **TaskManager.Infra**: Entity Framework Core data access and repository implementations.
- **TaskManager.Data**: Entity and DTO definitions.

---

## Features

- User, task, and status management
- Dynamic task types and status workflows
- Status transition validation per task type
- Change task status with business rules
- Audit fields (CreatedAt, UpdatedAt, UpdatedBy)
- API endpoints for all entities
- MVC and React frontends (React in progress)
- Clean architecture: separation of concerns, DI, repository & service patterns

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/) (for React frontend)
- SQL Server or SQLite (configurable in `appsettings.json`)

---

### 1. Restore and Install Dependencies

**From the solution root (where your `.sln` file is):**
	
	dotnet restore

	
**For the React frontend:**

	cd TaskManager.React/client npm install

---

### 2. Database Setup

1. Update the connection string in `TaskManager.APIServer/appsettings.json`.
2. Run EF Core migrations:```
dotnet ef database update --project TaskManager.Infra
```3. (Optional) Seed the database using the API:```
GET http://localhost:<port>/api/admin/init


### 3. Running the Solution  **API Server:**

cd TaskManager.APIServer dotnet run

cd TaskManager.MVC dotnet run


**React Frontend:**

cd TaskManager.React/client npm run dev


## API Endpoints

- `GET /api/user` — List users
- `GET /api/task` — List tasks
- `GET /api/tasktype` — List task types
- `GET /api/status` — List statuses
- `POST /api/task` — Create task
- `POST /api/task/{id}/status/{newStatusId}` — Change task status
- ...and more
- **Controllers**: API endpoints (APIServer)
- **Services**: Business logic (Logic)
- **Repositories**: Data access (Infra)
- **DTOs/Entities**: Data models (Data, Logic)
- **Views/Pages**: UI (MVC, React)


- 
## Development Notes

- Use dependency injection for all services and repositories.
- All dropdowns (users, task types, statuses) are populated dynamically from the API.
- The React frontend mirrors the MVC UI and API usage.
- For .NET dependencies, always run `dotnet restore` from the solution root.
- For React dependencies, always run `npm install` from `TaskManager.React/client`.

- ## Contributing

1. Fork the repo
2. Create a feature branch
3. Commit your changes
4. Open a pull request
 
