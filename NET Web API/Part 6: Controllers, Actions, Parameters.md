# Part 6 – Controllers, Actions, Parameters in ASP.NET Core Web API

---

## ✅ What is a Controller?

A **Controller** is a class that:
- Handles HTTP requests
- Inherits from `ControllerBase` (or `Controller` if you want Views)
- Contains **action methods**

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Products List");
}

```
---

## ✅ Action Method

An action is a public method inside a controller that:

- Matches a route
- Handles a specific HTTP verb (GET, POST, etc.)
- Returns `IActionResult`, `ActionResult<T>`, or a plain object

```csharp
[HttpGet("{id}")]
public IActionResult GetProduct(int id)
{
    return Ok($"Product {id}");
}
```

---

## ✅ Parameter Binding

ASP.NET Core binds parameters from:

- Route → `[FromRoute]`
- Query string → `[FromQuery]`
- Body → `[FromBody]`
- Header → `[FromHeader]`
- Form → `[FromForm]`

```csharp
[HttpPost]
public IActionResult Create([FromBody] ProductDto product)
```

---

## ✅ Action Result Return Types

| Type              | Description                          |
|-------------------|--------------------------------------|
| IActionResult     | Generic return (OK, NotFound, etc.)  |
| ActionResult<T>   | Strongly typed (auto-serializes)     |
| ObjectResult      | Custom status + content              |
| NoContentResult   | 204 No Content                       |

```csharp
[HttpGet("{id}")]
public ActionResult<ProductDto> Get(int id)
{
    var product = repo.Get(id);
    if (product == null) return NotFound();
    return product; // Automatically serialized
}
```

---

## ✅ Summary

- Controllers organize your API logic.
- Actions are endpoint methods tied to HTTP verbs.
- Use `[ApiController]` for better binding & validation.
- Return types control what the client receives.

---

# Part 7 – Middleware in ASP.NET Core

---

## ✅ What is Middleware?

Middleware is a component that:

- Processes HTTP requests and responses
- Forms the **request pipeline**
- Can **short-circuit** or **modify requests/responses**

---

## ✅ Built-in Middlewares

| Middleware              | Purpose                          |
|-------------------------|----------------------------------|
| `UseRouting()`          | Matches endpoints                |
| `UseAuthorization()`    | Enforces authorization policies  |
| `UseAuthentication()`   | Verifies user identity           |
| `UseCors()`             | CORS support                     |
| `UseExceptionHandler()` | Global error handling            |
| `UseStaticFiles()`      | Serve static content             |

---

## ✅ Custom Middleware Example

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Path}");
        await _next(context); // Call next middleware
    }
}
```

Register in Program.cs:

```csharp
app.UseMiddleware<RequestLoggingMiddleware>();
```

---

## ✅ Middleware Pipeline Order Matters

```csharp
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(...);
```

---

## ✅ When to Use Middleware vs Filters

| Use Case         | Middleware ✅ | Filters ✅ |
|------------------|---------------|------------|
| Global processing| ✅            | ❌         |
| Controller-specific | ❌         | ✅         |
| Early request check | ✅         | ❌         |
| Post-controller      | ❌         | ✅ (ResultFilter, etc.) |

---

## ✅ Summary

- Middleware builds the HTTP pipeline.
- Runs globally for all requests.
- Custom middleware gives full control.
- Filters are better for controller-specific behavior.

---

# Part 8 – Security: Authentication, Authorization, and JWT

---

## ✅ Authentication vs Authorization

| Concept        | Purpose                        |
|----------------|--------------------------------|
| Authentication | Verifies *who* the user is     |
| Authorization  | Verifies *what* the user can do|

---

## ✅ Use JWT (JSON Web Token) for Authentication

JWT is a token format used to authenticate API users in a **stateless** way.

---

### 🔹 Install JWT NuGet Package

```bash
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
```

---

### 🔹 Configure JWT in Program.cs

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourapi.com",
            ValidAudience = "yourapi.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey"))
        };
    });
```

---

### 🔹 Add to Middleware

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

---

### 🔹 Use [Authorize] Attribute

```csharp
[Authorize]
[HttpGet]
public IActionResult GetSecureData() => Ok("You are authorized");
```

---

### 🔹 Role-Based Authorization

```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminOnly() => Ok("Admin section");
```

---

## ✅ Summary

- Use JWT for stateless API authentication
- Add JWT Bearer middleware
- Secure endpoints using `[Authorize]`
- Control access using roles and policies

---

# Part 9 – Logging and Monitoring in ASP.NET Core

---

## ✅ Built-in Logging

ASP.NET Core provides logging via `ILogger<T>` interface.

---

### 🔹 Inject ILogger

```csharp
private readonly ILogger<ProductsController> _logger;

public ProductsController(ILogger<ProductsController> logger)
{
    _logger = logger;
}

[HttpGet]
public IActionResult Get()
{
    _logger.LogInformation("Fetching products");
    return Ok("Products");
}
```

---

### 🔹 Logging Levels

| Level      | Purpose                          |
|------------|----------------------------------|
| Trace      | Detailed debug info              |
| Debug      | Development-only diagnostics     |
| Information| General application events       |
| Warning    | Unexpected events                |
| Error      | Errors that allow continuation   |
| Critical   | Fatal application failure        |

---

## ✅ Structured Logging with Serilog

```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

---

## ✅ Monitoring

- Use Application Insights (Azure) or CloudWatch (AWS)
- Add health checks with:

```csharp
services.AddHealthChecks();
app.MapHealthChecks("/health");
```

---

## ✅ OpenTelemetry (Advanced)

- Distributed tracing (spans, services)
- Integrate with Grafana, Jaeger

---

## ✅ Summary

- Use `ILogger<T>` for logging
- Use Serilog for structured logs
- Add health checks for monitoring
- Use Application Insights, OpenTelemetry for full observability

