# .NET Web API Deep Dive (Part 4)

---

## ✅ What are Filters in ASP.NET Core?

**Filters** are components that run **before or after** certain stages in the request pipeline. They provide a way to add **cross-cutting concerns** (like logging, caching, exception handling, validation, authorization) across multiple controllers or actions.

---

## 🔹 Types of Filters

| Filter Type        | Interface                 | Use Case                               |
|--------------------|---------------------------|-----------------------------------------|
| Authorization      | `IAuthorizationFilter`    | Access control before executing action  |
| Action             | `IActionFilter`           | Logic before/after controller actions   |
| Result             | `IResultFilter`           | Logic before/after result processing    |
| Exception          | `IExceptionFilter`        | Global exception handling               |
| Resource           | `IResourceFilter`         | Short-circuiting before model binding   |
| Async Versions     | `*FilterAsync` interfaces | Support `await` in filters              |

---

## 🔹 Filter Execution Flow

Request Pipeline Order:

1. Authorization Filters  
2. Resource Filters  
3. Action Filters  
4. Controller Action Executes  
5. Result Filters  
6. Exception Filters (can wrap everything)

---

## ✅ Action Filter Example

```csharp
public class LoggingActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Before Action executes");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("After Action executes");
    }
}

```
---


# Filters in ASP.NET Core Web API

## Registering Globally

```csharp
services.AddControllers(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});
```

## Or apply to a controller/action

```csharp
[ServiceFilter(typeof(LoggingActionFilter))]
```

---

## ✅ Authorization Filter Example

```csharp
public class RoleCheckFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.IsInRole("Admin"))
        {
            context.Result = new ForbidResult();
        }
    }
}
```
Used to enforce custom access control.

---

## ✅ Exception Filter Example

```csharp
public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult("An error occurred")
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}
```

---

## ✅ Result Filter Example

```csharp
public class AddHeaderResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("X-Processed-By", "API");
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}
```
Used for response modification.

---

## ✅ Resource Filter Example

```csharp
public class CachingResourceFilter : IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        // Check cache
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        // Save response to cache
    }
}
```
Resource filters execute before model binding.

---

## 🔹 Attribute vs Service Filters vs Type Filters

| Type            | Registered As                   | When to Use                    |
|-----------------|----------------------------------|--------------------------------|
| Attribute Filter| `[CustomFilter]`                | Simple, no DI required         |
| Service Filter  | `[ServiceFilter(typeof(...))]`  | Need DI inside filter          |
| Type Filter     | `[TypeFilter(typeof(...))]`     | Need constructor args in filter|

---

## 🔸 Basic Interview Questions

### Q1: What is the purpose of filters in Web API?
To inject cross-cutting logic like:
- Logging
- Authorization
- Exception handling
- Response customization

### Q2: What is the difference between IActionFilter and IResultFilter?
- `IActionFilter`: Runs before and after an action method.
- `IResultFilter`: Runs before and after the result is returned to the client.

### Q3: What is the order of execution of filters?
1. `IAuthorizationFilter`
2. `IResourceFilter`
3. `IActionFilter`
4. Action method
5. `IResultFilter`
6. `IExceptionFilter`

---

## 🔸 Advanced Interview Questions

### Q4: What is the difference between middleware and filters?

| Feature    | Middleware           | Filters                     |
|------------|----------------------|-----------------------------|
| Scope      | Global pipeline       | MVC controllers/actions     |
| Runs       | Before routing        | After routing               |
| Use Cases  | Logging, CORS, Auth headers | Validation, model checks  |

### Q5: How do you implement dependency injection in a filter?
Use `ServiceFilter` or `TypeFilter`:

```csharp
[ServiceFilter(typeof(MyFilter))]
```

Register it in `Startup.cs`:

```csharp
services.AddScoped<MyFilter>();
```

### Q6: Can filters be asynchronous?
Yes. Implement `IAsyncActionFilter`, `IAsyncExceptionFilter`, etc.

```csharp
public class AuditFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Before
        var result = await next(); // Proceed to next filter or action
        // After
    }
}
```

---

## ✅ Summary
- Filters are powerful for cross-cutting logic in Web APIs.
- Choose the correct filter type depending on lifecycle need.
- Use async filters when awaiting async operations.
- Use DI via `ServiceFilter` or `TypeFilter` for advanced scenarios.

