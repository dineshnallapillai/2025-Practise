# .NET Web API Deep Dive (Part 5)

---

## ✅ API Versioning in ASP.NET Core

API versioning is important for **backward compatibility** when APIs evolve over time.

### 🔹 Versioning Strategies:

| Type           | Example                          |
|----------------|----------------------------------|
| URI            | `/api/v1/products`               |
| Query String   | `/api/products?api-version=1.0`  |
| Header         | `api-version: 1.0`               |
| Media Type     | `Accept: application/json;v=1.0` |

---

### 🔹 Install NuGet Package

```bash
Install-Package Microsoft.AspNetCore.Mvc.Versioning

---

## 🔹 Configure Versioning

```csharp
services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version")
    );
});
```

## 🔹 Use in Controller

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
public class ProductsV1Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("V1 products");
}
```

## ✅ Output Formatting (JSON / XML)
ASP.NET Core uses formatters to convert responses to JSON, XML, etc.

### 🔹 Configure XML Formatters

```csharp
services.AddControllers()
        .AddXmlSerializerFormatters();
```

Now your API can respond with XML if:
```
Accept: application/xml
```

### 🔹 Custom Media Type

```csharp
services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
```

## ✅ Response Caching (Output Caching)
Response caching stores HTTP responses so future requests can be served faster.

### 🔹 Output Caching Middleware (.NET 8+)

```csharp
app.UseOutputCache(); // Enable middleware

app.MapGet("/products", GetProducts)
   .CacheOutput(x => x.Expire(TimeSpan.FromSeconds(60)));
```

- Built-in in .NET 8
- Works for GET endpoints
- Honors cache headers like Cache-Control

### 🔹 In-Memory Cache Example

```csharp
services.AddMemoryCache();

[HttpGet("{id}")]
public IActionResult Get(int id, [FromServices] IMemoryCache cache)
{
    var product = cache.GetOrCreate($"product-{id}", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return service.GetProduct(id);
    });
    return Ok(product);
}
```

## ✅ Rate Limiting in Web API
Helps prevent abuse and overload by limiting request frequency from clients.

### 🔹 Popular Libraries
- AspNetCoreRateLimit

### 🔹 Install NuGet Package
```bash
Install-Package AspNetCoreRateLimit
```

### 🔹 Configuration (appsettings.json)

```json
"IpRateLimiting": {
  "EnableEndpointRateLimiting": true,
  "StackBlockedRequests": false,
  "RealIpHeader": "X-Real-IP",
  "ClientIdHeader": "X-ClientId",
  "HttpStatusCode": 429,
  "GeneralRules": [
    {
      "Endpoint": "*",
      "Period": "1m",
      "Limit": 5
    }
  ]
}
```

### 🔹 In Startup.cs

```csharp
services.AddMemoryCache();
services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
services.AddInMemoryRateLimiting();
services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

app.UseIpRateLimiting();
```

## 🔸 Basic Interview Questions

**Q1: Why do we need API versioning?**  
To avoid breaking changes for consumers using older versions while evolving the API.

**Q2: What versioning styles are supported?**
- URL path versioning
- Query string
- Request headers
- Media types (Accept headers)

**Q3: How do you configure XML as a supported output?**  
Use `.AddXmlSerializerFormatters()` in Startup.cs to support `application/xml`.

**Q4: What is output caching?**  
Stores and reuses server responses for GET requests to reduce server load and improve performance.

**Q5: Why is rate limiting important?**
- Prevents API abuse
- Protects backend resources
- Enforces fair usage for all clients

## 🔸 Advanced Interview Questions

**Q6: What are the benefits of UseOutputCache() in .NET 8?**
- No external library required
- Clean integration with minimal APIs
- Honors cache headers
- Easy to configure expiration and vary-by rules

**Q7: What’s the difference between memory cache and output cache?**

| Feature        | Memory Cache           | Output Cache             |
|----------------|------------------------|--------------------------|
| Purpose        | Store any in-memory object | Store HTTP response  |
| Layer          | Application logic layer   | HTTP middleware         |
| Control        | Full control in code      | Declarative caching at endpoint |

**Q8: How would you cache data that changes frequently?**
- Use sliding expiration or absolute expiration
- Use ETags and conditional GET
- Combine with Redis or distributed cache

**Q9: How would you implement rate limiting per user or API key?**
- Use `ClientIdHeader` in AspNetCoreRateLimit
- Store and track client request count via cache (e.g., Redis)

## ✅ Summary

- Use API versioning to evolve APIs safely.
- Use output caching and memory caching to optimize performance.
- Format responses as JSON/XML using content negotiation.
- Protect APIs with rate limiting for fairness and security.
