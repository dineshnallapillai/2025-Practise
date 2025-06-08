# .NET Web API Deep Dive (Part 3)

---

## ✅ What is Model Binding?

Model Binding is the process by which ASP.NET Core **maps incoming HTTP request data to method parameters** in controller actions.

### Common Binding Sources:
- `[FromRoute]`
- `[FromQuery]`
- `[FromBody]`
- `[FromHeader]`
- `[FromForm]`

---

### Example:

```csharp
[HttpGet("search")]
public IActionResult Search([FromQuery] string keyword)
{
    return Ok($"Searching for: {keyword}");
}

[HttpPost]
public IActionResult Create([FromBody] ProductDto product)
{
    return Ok($"Created product: {product.Name}");
}

```
---


# Model Binding, Validation & Exception Handling in ASP.NET Core

---

## 🔹 Binding Source Attributes

| Attribute     | Binds From          | Example                     |
|---------------|---------------------|-----------------------------|
| `[FromQuery]` | URL query string    | `/api/products?name=tv`     |
| `[FromRoute]` | Route parameters    | `/api/products/123`         |
| `[FromBody]`  | Request body        | JSON or XML payload in POST |
| `[FromForm]`  | Form fields         | Form-encoded data (POST)    |
| `[FromHeader]`| HTTP headers        | Authorization, X-Custom-Id  |

---

## ✅ What is Model Validation?

Model validation ensures that incoming data is valid before the controller logic executes. It uses:

- Data Annotations (e.g., `[Required]`, `[Range]`)
- FluentValidation (for complex scenarios)

### Example

```csharp
public class ProductDto
{
    [Required]
    public string Name { get; set; }

    [Range(1, 100000)]
    public decimal Price { get; set; }
}

[HttpPost]
public IActionResult AddProduct([FromBody] ProductDto product)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    return Ok("Product added");
}
```

---

## 🔹 Automatic Model Validation in ASP.NET Core

Using `[ApiController]` enables automatic model validation:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(ProductDto dto) => Ok(dto);
}
```

If the model is invalid, ASP.NET Core automatically returns **400 Bad Request**.

---

## ✅ Exception Handling in ASP.NET Core Web API

Exception handling catches unexpected runtime errors and returns meaningful responses.

### 1. Try-Catch in Controller

```csharp
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    try
    {
        var product = service.Get(id);
        return Ok(product);
    }
    catch (NotFoundException ex)
    {
        return NotFound(ex.Message);
    }
}
```

### 2. Global Exception Handling with Middleware

```csharp
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Details = contextFeature.Error.Message
            }.ToString());
        }
    });
});
```

### 3. Custom Exception Filter

```csharp
public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult("An error occurred")
        {
            StatusCode = 500
        };
    }
}

// Register globally
services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});
```

---

## 🔸 Basic Interview Questions

### Q1: What is model binding?

Model binding maps incoming HTTP data (query string, route, headers, body) to action method parameters.

---

### Q2: What happens if model validation fails?

- With `[ApiController]`: ASP.NET returns 400 Bad Request automatically.
- Without it: You must check `ModelState.IsValid` manually.

---

### Q3: Difference between `[FromBody]` and `[FromQuery]`?

| Attribute     | Data Source    | Example                          |
|---------------|----------------|----------------------------------|
| `[FromBody]`  | Request body   | `POST /api/products { "name": "TV" }` |
| `[FromQuery]` | Query string   | `GET /api/products?name=TV`      |

---

### Q4: Why use `[ApiController]`?

- Automatic model validation
- Binding source inference
- Better error messages
- Required attribute enforcement

---

## 🔸 Advanced Interview Questions

### Q5: How do you handle different exception types centrally?

- Use custom exception classes (e.g., `NotFoundException`)
- Handle them with middleware or filters

---

### Q6: How do you return custom error messages from validation?

```csharp
services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

        return new BadRequestObjectResult(new { Errors = errors });
    };
});
```

---

### Q7: Difference between Exception Filter and Exception Middleware?

| Feature              | Exception Middleware | Exception Filter         |
|----------------------|----------------------|---------------------------|
| Layer                | Global (pipeline)    | MVC controller/action     |
| Handles all errors   | Yes                  | Only MVC exceptions       |
| Preferred for        | General API handling | Domain-level error formatting |

---

## ✅ Summary

- Use **model binding** to map request data to parameters.
- Validate input using **data annotations** or **FluentValidation**.
- Centralize error handling using **middleware** or **filters**.
- Prefer `[ApiController]` for **automatic validation responses**.
