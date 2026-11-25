# MovieDb

ASP.NET MVC 5 + Entity Framework 6 movie catalog application (Movies, Actors, Producers) running in Docker with Mono (`xsp4`) and SQL Server 2022 for Linux.

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Linux containers)

## Quick Start

```bash
docker compose up -d --build
```

Once the `moviedb-sql` healthcheck passes and `moviedb-web` starts, open:

- **Web App:** http://localhost:8080

Entity Framework Code First automatically creates the `MovieDb` database and tables on the first request.

## Configuration

Optional environment variables (override via `.env` or shell):

| Variable | Default | Description |
| --- | --- | --- |
| `WEB_PORT` | `8080` | Host port mapped to the web container |
| `MSSQL_PORT` | `1433` | Host port mapped to SQL Server |
| `MSSQL_SA_PASSWORD` | `MovieDb_Pass123!` | SQL Server `sa` password (must meet SQL Server complexity rules) |

The web app reads `DB_CONNECTION_STRING` from the environment (falling back to `DbConnectionString` in `MovieDb/Web.config` when running outside Docker).

## Stopping & Cleanup

```bash
# Stop containers (preserves database and uploaded posters)
docker compose down

# Stop containers and remove volumes
docker compose down -v
```

## License

MIT — see [LICENSE](LICENSE).
