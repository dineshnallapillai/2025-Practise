ASP.NET Core Request Pipeline

Client → Middleware → Routing → Endpoint (Controller or Minimal API) → Filters → 
                                                                           Action → Result → Middleware → Response

Key Components:
Program.cs: Entry point, service registration & middleware setup.

Middleware: Executes in sequence. Can short-circuit or pass downstream.

Endpoint: Controller or minimal API endpoint

Model Binding: Maps incoming data to C# objects

Filters: Action, Exception, Authorization logic

Result Execution: JSON, XML, or custom result returned

Controllers (Traditional)

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase {
    [HttpGet("{id}")]
    public IActionResult Get(int id) => Ok(new { id });
}

Minimal APIs (Modern)
app.MapGet("/api/products/{id}", (int id) => Results.Ok(new { id }));

Use Case	Recommended
REST APIs	Controllers
Lightweight/internal APIs	Minimal APIs
Full control over HTTP pipeline	Minimal APIs

Routing
Attribute-based: [Route("api/[controller]")]

Constraints: [HttpGet("{id:int:min(1)}")]

Filters
Action Filter – Pre/Post logic around controller actions

Exception Filter – Global error handling

Authorization Filter – Custom policy enforcement

Dependency Injection (DI) & Lifetimes

Lifetime	Use Case
Singleton	One instance for app lifetime
Scoped	One per request
Transient	New every time

📘 Best Practice:

Avoid injecting DbContext as Singleton

Use IOptions<T> for configuration binding

OpenAPI/Swagger

NET 9 deprecates Swashbuckle, so also explore OpenAPI with NSwag or Scalar.

Real-World REST API Design Principles

REST Best Practices:
Use nouns, not verbs: /products not /getAllProducts

Use status codes properly

Use pagination for collections

Use Location header after POST

Use HATEOAS (optional but ideal for hypermedia-driven APIs)