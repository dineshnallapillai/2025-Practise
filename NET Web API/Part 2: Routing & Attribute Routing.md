# .NET Web API Deep Dive (Part 2)

---

## ✅ What is Routing in ASP.NET Core?

Routing in ASP.NET Core maps **incoming HTTP requests to route handlers** (like controllers or minimal APIs). It determines *which method gets executed* for a given URL.

There are two types of routing:
1. **Convention-based Routing** (used in MVC)
2. **Attribute Routing** (commonly used in Web APIs)

---

## 🔹 1. Attribute Routing

With **attribute routing**, you define routes directly on controller actions using attributes like `[Route]`, `[HttpGet]`, `[HttpPost]`, etc.

### Example:

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        return Ok($"Product {id}");
    }

    [HttpPost]
    public IActionResult AddProduct(ProductDto product)
    {
        return CreatedAtAction(nameof(GetProduct), new { id = 101 }, product);
    }
}

```
---


## 2. Routing Tokens

- `[controller]` → Replaced with controller name (e.g., `Products`)
- `[action]` → Replaced with action method name
- `{parameter}` → URL path variables

### Example:
```csharp
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Profile(int id) => Ok($"User {id}");
}
// Route: GET /api/user/profile/5
```

---

## 3. Route Constraints

You can restrict parameters using route constraints:

```csharp
[HttpGet("{id:int}")]
public IActionResult GetProductById(int id) { ... }

[HttpGet("{name:alpha}")]
public IActionResult GetProductByName(string name) { ... }
```

- `int` → Only match integers  
- `alpha` → Only match alphabetic strings

**Common Constraints**: `int`, `bool`, `datetime`, `guid`, `length(x)`, `min(x)`, `max(x)`

---

## 4. Optional Parameters & Defaults

```csharp
[HttpGet("{page:int=1}")]
public IActionResult GetPaged(int page = 1) => Ok($"Page {page}");
```

- Route `/products` → Page 1  
- Route `/products/3` → Page 3

---

## 5. Multiple Route Templates

You can assign multiple routes to the same action:

```csharp
[HttpGet("product/{id}")]
[HttpGet("item/{id}")]
public IActionResult GetById(int id) => Ok($"ID = {id}");
```

- Both `/product/1` and `/item/1` will work.

---

## 6. Route Prefixes with `[Route]` at Controller Level

Define a base route at the controller:

```csharp
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok("List");

    [HttpGet("{id}")]
    public IActionResult Get(int id) => Ok($"Product {id}");
}
```

**Full Routes:**

- `GET /api/products`  
- `GET /api/products/101`

---

## 🔸 Basic Interview Questions

### Q1: What’s the difference between attribute routing and convention-based routing?

| Feature             | Attribute Routing              | Convention-based Routing         |
|---------------------|--------------------------------|----------------------------------|
| **Location**        | On controller/action           | In `Startup.cs` or `Program.cs` |
| **Flexibility**     | High (custom paths)            | Simple, pattern-matching        |
| **Use Case**        | Web APIs                       | MVC (web pages)                 |

---

### Q2: What does `[Route("api/[controller]")]` mean?

It’s a route prefix where `[controller]` is replaced by the controller class name minus "Controller".  
**Example**: `ProductsController` → `api/products`

---

### Q3: What is the purpose of `[HttpGet("{id:int}")]`?

It restricts the route to accept only integer values for the `id` parameter.

---

### Q4: Can one action support multiple routes?

Yes, using multiple `[HttpGet(...)]` attributes on the same method.

---

## 🔸 Advanced Interview Questions

### Q5: How are routes matched in Web API?

The routing engine evaluates all defined routes and picks the **first one** that matches the request method and URL pattern.

---

### Q6: How do you handle conflicting routes?

- Be explicit in route definitions.  
- Use constraints and route order carefully.  
- Avoid overlapping patterns like:

```csharp
[HttpGet("{id}")]
[HttpGet("{name}")]
```

---

### Q7: What happens if two routes match a request?

A runtime error will occur: `AmbiguousMatchException`.  
**Solution**: Use route constraints or make routes more specific.

---

### Q8: How do you customize route behavior globally?

Use `RouteOptions` in `Startup.cs`:

```csharp
services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
```

---

## ✅ Summary

- Attribute routing gives you full control over URL paths.
- Route tokens and constraints make routing precise and flexible.
- You should avoid ambiguities and use route prefixes for cleaner design.

