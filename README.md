# CommerciumApp

A web API (ASP.NET Core, .NET 8) backed by a PostgreSQL database. The database and pgAdmin run in containers via Podman.

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Podman](https://podman.io/) with `podman compose` (or `podman-compose`)
- EF Core CLI tool:

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
```

## Project structure

.
├── api/ # ASP.NET Core Web API
├── compose.yaml # PostgreSQL + pgAdmin
├── .env.example # template for passwords
└── .env # your passwords (NOT committed to Git)

## Getting started

### 1. Container passwords

In the project root, copy the template and fill in your own passwords:

```bash
cp .env.example .env
```

Set the following in `.env`:
POSTGRES_PASSWORD=your_database_password
PGADMIN_PASSWORD=your_pgadmin_password

`.env` is listed in `.gitignore` and must never be committed.

### 2. Start the database and pgAdmin

On macOS, start the Podman virtual machine first:

```bash
podman machine start
```

Then, from the project root:

```bash
podman compose up -d
podman compose ps
```

| Service  | Address               |
| -------- | --------------------- |
| Database | `localhost:5432`      |
| pgAdmin  | http://localhost:5050 |

### 3. Connection string (user-secrets)

The database password is not stored in `appsettings.json`. It is kept in .NET user-secrets. From the `api` folder, run:

```bash
cd api
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=commerciumdb;Username=postgres;Password=YOUR_PASSWORD"
```

The password must match `POSTGRES_PASSWORD` in `.env`. The `UserSecretsId` is already in `api.csproj`, so `dotnet user-secrets init` is not needed. To verify:

```bash
dotnet user-secrets list
```

Note: user-secrets only work in the `Development` environment and are stored per machine, so they must be set up again on every new computer.

### 4. Migrations

From the `api` folder:

```bash
dotnet ef database update
```

This creates the tables in the database. To add a new migration:

```bash
dotnet ef migrations add MigrationName
```

### 5. Run the API

```bash
dotnet run
```

## pgAdmin

1. Open http://localhost:5050 and sign in with the email from `compose.yaml` and the `PGADMIN_PASSWORD` value.
2. Right-click **Servers → Register → Server**.
3. On the **Connection** tab, enter:
   - Host: `db` (the service name from `compose.yaml`, not `localhost`)
   - Port: `5432`
   - Maintenance database: `commerciumdb`
   - Username: `postgres`
   - Password: the `POSTGRES_PASSWORD` value

## Useful commands

```bash
podman compose up -d          # start containers
podman compose down           # stop containers (data is kept)
podman compose down -v        # stop containers and delete data (volumes)
podman compose logs db        # database logs
```

## Troubleshooting

- **Changing the database password has no effect.** Postgres only reads the password when the volume is first created. Run `podman compose down -v` and then `podman compose up -d` (this deletes all data).
- **Port 5432 is already in use.** In `compose.yaml`, change the left-hand number (e.g. `5433:5432`) and set `Port=5433` in the connection string.
- **The app reports a missing connection string.** Make sure the user-secrets key (`ConnectionStrings:Default`) matches the name used in `GetConnectionString("Default")` in `Program.cs`.
- **Package versions.** All EF Core packages (`Microsoft.EntityFrameworkCore.*`, `Npgsql.EntityFrameworkCore.PostgreSQL`) must share the same major version (8.0.x).

## Security notes

- Never commit `.env`, passwords, or connection strings containing passwords.
- Replace default passwords with strong, random values.
- In production, use environment variables or a secrets manager instead of user-secrets.
- In production, the application should use a dedicated database user with limited privileges rather than `postgres`.
