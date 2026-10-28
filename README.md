# HouseAPP

An ASP.NET Core Web API for managing house listings (name, square footage, occupancy, image URL and amenities), backed by SQL Server through Entity Framework Core and a generic repository.

## Tech Stack

- .NET 8 (`net8.0`)
- ASP.NET Core Web API with Newtonsoft.Json
- Entity Framework Core 8 with SQL Server
- AutoMapper 13
- Serilog (file sink)
- Swashbuckle (Swagger UI)

## Project Structure

The solution `HousesAPI.sln` contains a single project, `HousesApp`:

| Folder | Description |
| --- | --- |
| `Controllers` | `HouseController` with the CRUD endpoints. |
| `Db` | `HousesDb` EF Core DbContext. |
| `Models` | `HouseModel` entity and the `HouseDto`, `HouseCreateDto` and `UpdateHouseDto` DTOs. |
| `Mapping` | AutoMapper profile between the entity and the DTOs. |
| `Repository` | Generic `Repository<T>` and `HouseRepository`. |
| `Response` | `ApiResponse` wrapper returned by the endpoints. |
| `Migrations` | EF Core migrations that create the `houses` table. |

## Features

- Create, read, update and delete houses.
- Responses wrapped in a common `ApiResponse` (`StatusCode`, `isSuccess`, `ErrorMessages`, `Result`).
- DTO validation with data annotations.
- Error logging to a file with Serilog.
- Swagger UI in the Development environment.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (or another SQL Server instance)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Configuration

The connection string is in `HousesApp/appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Data Source=(localdb)\\MSSQLLocalDB;TrustServerCertificate=True;Database=HouseWebApp;Integrated Security=True"
}
```

Change it if you use a different SQL Server instance.

### Create the database

```bash
cd HousesApp
dotnet ef database update
```

### Run

```bash
cd HousesApp
dotnet run --launch-profile https
```

The API listens on `https://localhost:7253` and `http://localhost:5044`; Swagger UI is at `/swagger`.

## API Endpoints

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/House/all` | Get all houses. |
| GET | `/api/House/{id}` | Get a house by id. |
| POST | `/api/House/create` | Create a house. |
| PUT | `/api/House/{id}` | Update a house. |
| DELETE | `/api/House?id={id}` | Delete a house. |
