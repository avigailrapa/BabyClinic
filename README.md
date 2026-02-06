# BabyClinic Web API

Lightweight ASP.NET Core Web API for managing babies, nurses and appointments. Includes JWT authentication, role-based authorization, file uploads (form), AutoMapper and EF migrations.

## Quick start

1. Clone repository
   - git clone https://github.com/avigailrapa/BabyClinic

2. Configure
   - Update the connection string in `lesson6/appsettings.json` under `ConnectionStrings:database-home`.
   - Update JWT settings in `lesson6/appsettings.json` (`Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`).

3. Prepare database
   - From the project root run:
     - `dotnet restore`
     - `dotnet build`
     - `dotnet ef database update`
   - Or use Visual Studio 2026: open the solution and run __Build__ then apply migrations using the Package Manager Console or __Tools > NuGet Package Manager__ workflows.

4. Run
   - From CLI: `dotnet run --project lesson6`
   - From Visual Studio 2026: set the `lesson6` project as the startup project and press __Run__ (or __Debug__).

Swagger UI is available in Development environment.

## Authentication

- Login endpoints:
  - `POST /api/baby/login` (body: `Login` DTO) — returns a JWT for babies with role `user`.
  - `POST /api/nurse/login` (body: `Login` DTO) — returns a JWT for nurses with role `nurse`.
- Token lifetime: 15 minutes.
- Send token in requests using the `Authorization` header:
  - `Authorization: Bearer {token}`

JWT configuration keys are read from `lesson6/appsettings.json`:
- `Jwt:Key`
- `Jwt:Issuer`
- `Jwt:Audience`

## API endpoints

- Babies
  - `GET /api/baby` — requires role `user` (protected)
  - `GET /api/baby/{id}`
  - `POST /api/baby` — form upload supported (image file saved to `images/`)
  - `PUT /api/baby/{id}`
  - `DELETE /api/baby/{id}`

- Nurses
  - `GET /api/nurse` — requires role `nurse` (protected)
  - `GET /api/nurse/{id}`
  - `POST /api/nurse` — form data
  - `PUT /api/nurse/{id}`
  - `DELETE /api/nurse/{id}`

- Appointments
  - `GET /api/appointment`
  - `GET /api/appointment/{id}`
  - `POST /api/appointment` — form data
  - `PUT /api/appointment/{id}`
  - `DELETE /api/appointment/{id}`

Request and response payload shapes (DTOs) are in `Common/Dto` (e.g., `BabyDto`, `NurseDto`, `AppointmentDto`).

## Notes & implementation details

- Project entry: `lesson6/Program.cs`
  - Registers JWT authentication, AutoMapper and application services.
  - Database context is created via `IContext` and the connection string `database-home`.
- File uploads:
  - `BabyController` saves uploaded images to `images/` under the current working directory.
- Role-based authorization is applied using `Authorize(Roles = "user")` and `Authorize(Roles = "nurse")`.
- Swagger (OpenAPI) is enabled in Development to test endpoints and to supply bearer tokens.

## Troubleshooting

- If Swagger does not show protected endpoints, ensure the environment is `Development`, or supply a valid JWT in the Authorization input.
- If migrations fail, confirm the connection string and SQL Server availability, and run `dotnet ef migrations list` to inspect available migrations.

## Where to look in the codebase

- Startup / configuration: `lesson6/Program.cs`
- Controllers: `lesson6/Controllers/*Controller.cs`
- DTOs: `Common/Dto/*.cs`
- Repositories: `Repository/Repositories/*Repository.cs`
- Entity models & DbContext: `Repository/Entities` and `DataContext/BabyClinicContext.cs`
- AutoMapper profile: `Service/Services/MapperProfile.cs`

