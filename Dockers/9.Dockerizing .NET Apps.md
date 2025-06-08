# 9.1 Prerequisites
- .NET 8 SDK installed
- Docker Desktop running
- A sample .NET app (we’ll generate one)

---

## 🔹 Part A: Dockerizing an ASP.NET Core Web API

### 🧱 9.2 Create a .NET Web API

```bash
dotnet new webapi -n MyWebApi
cd MyWebApi
```
Run locally to test:
```bash
dotnet run
```
---
# 9.3 Add a Dockerfile

In `MyWebApi/`, create:

```Dockerfile
# ---------------------
# Multi-stage Dockerfile
# ---------------------

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyWebApi.dll"]
```
---
# 9.4 Build and Run the Image
```bash
docker build -t mywebapi .
docker run -d -p 5000:80 --name webapi mywebapi
```
📌 Visit: http://localhost:5000/swagger

# 9.5 Test with Docker Compose (Optional)
```yaml
# docker-compose.yml
version: '3.8'
services:
  web:
    build: .
    ports:
      - "5000:80"
```
Run:
```bash
docker-compose up
```
### Part B: Dockerizing .NET Console App

# 9.6 Create Console App
```bash
dotnet new console -n HelloDocker
cd HelloDocker
```
Example Program.cs:
```bash
// Program.cs
Console.WriteLine("Hello from Dockerized .NET Console App!");
```
# Dockerfile
```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out
CMD ["dotnet", "out/HelloDocker.dll"]
```
Build and run:
```bash
docker build -t hellodocker .
docker run hellodocker
```
✅ Output:
```bash
Hello from Dockerized .NET Console App!
```
### Part C: Dockerizing a .NET Worker Service

Worker services are great for background jobs or daemon processes.
```bash
dotnet new worker -n MyWorker
cd MyWorker
```
# Dockerfile (multi-stage):
```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "MyWorker.dll"]
```
Run with:
```bash
docker build -t myworker .
docker run myworker
```
✅ Output: Logs show background tasks every few seconds.

# Best Practices

| Practice            | Description                               |
|---------------------|-------------------------------------------|
| Multi-stage builds  | Smaller, production-grade images          |
| .dockerignore       | Exclude `bin/`, `obj/`, `.git` from build context |
| ENTRYPOINT vs CMD   | Use `ENTRYPOINT` for main command          |
| Health checks       | Add in `docker-compose.yml` or `Dockerfile` |
| ENV variables       | Use `ENV` or `docker-compose.env` for secrets/configs |
| Use dotnet publish  | Never use `dotnet run` in Dockerfile        |
| Expose Ports Properly | `EXPOSE 80` helps with docs & tooling     |

---

# Bonus: Docker Compose for .NET API + DB

```yaml
version: '3.8'
services:
  api:
    build: .
    ports:
      - "5000:80"
    environment:
      - ConnectionStrings__Db=Host=db;Port=5432;Username=postgres;Password=pass
    depends_on:
      - db

  db:
    image: postgres
    environment:
      POSTGRES_PASSWORD: pass
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```
| App Type    | Command to Create      | Dockerfile Base Image                  |
|-------------|-----------------------|---------------------------------------|
| Web API     | dotnet new webapi     | aspnet (runtime), sdk (build)         |
| Console App | dotnet new console    | sdk                                   |
| Worker App  | dotnet new worker     | runtime (multi-stage recommended)     |

















