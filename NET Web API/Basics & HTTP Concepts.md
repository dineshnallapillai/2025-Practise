# .NET Web API Deep Dive (Part 1)

---

## ✅ What is ASP.NET Web API?

ASP.NET Web API is a framework for building RESTful HTTP services using the .NET platform. It supports features like:
- Routing
- HTTP verbs (GET, POST, PUT, DELETE)
- Content negotiation (JSON, XML)
- Middleware, Filters, Model Binding

---

## 🔹 Basic Interview Questions

### Q1: What is the difference between Web API and MVC?
| Aspect       | Web API                      | MVC                         |
|--------------|------------------------------|------------------------------|
| Purpose      | Build REST services          | Build Web apps (HTML + View) |
| Return Types | `IActionResult`, JSON, XML   | `ViewResult`, HTML Views     |
| UI Layer     | No                           | Yes                          |

---

### Q2: What are the main HTTP verbs and their purposes?
- **GET**: Retrieve data/resource  
- **POST**: Create a new resource  
- **PUT**: Replace the resource completely  
- **PATCH**: Partially update the resource  
- **DELETE**: Delete the resource  
- **OPTIONS**: Pre-flight request (CORS)

---

### Q3: What is REST and how is it implemented in Web API?

REST (Representational State Transfer) is an architectural style for networked applications. Key principles:
- Resources are identified by URIs.
- Uses standard HTTP verbs.
- Stateless interactions.
- Responses typically in JSON/XML.

In Web API, REST is implemented using:
- Routing (e.g. `/api/products/1`)
- HTTP methods (`[HttpGet]`, `[HttpPost]`)
- Model binding and response formatting

---

### Q4: What is Content Negotiation?

Content negotiation is the process by which the server selects the best response format based on the `Accept` header sent by the client.

In ASP.NET Core:
```csharp
services.AddControllers()
        .AddXmlSerializerFormatters(); // Adds support for XML

The server then returns application/json or application/xml depending on client request headers

---
## Q5: What is the difference between REST and SOAP?

| Feature      | REST                               | SOAP                               |
|--------------|------------------------------------|-------------------------------------|
| Protocol     | HTTP                               | HTTP, SMTP, TCP                     |
| Format       | JSON, XML                          | XML only                            |
| Lightweight  | Yes                                | No                                  |
| Stateless    | Yes                                | Not always                          |
| Contract     | URI-based, less formal             | Strong WSDL/XSD contracts           |
| Use Case     | Web/mobile apps, microservices     | Enterprise apps, legacy systems     |

---

## 🔸 Advanced Interview Questions

### Q6: What are the key characteristics of RESTful services?

- **Client-server architecture**
- **Statelessness**
- **Cacheable responses**
- **Uniform interface**
- **Layered system**
- **Code-on-demand** (optional)

---

### Q7: When would you not use REST?

You might avoid REST in cases like:

- **Real-time communication** → Prefer **WebSockets**
- **Streaming or contract-based APIs** → Prefer **gRPC** or **SOAP**
- **Binary serialization or high performance** → Prefer **gRPC**
- **Strong security/ACID transactions** → Consider **SOAP** or other protocols

---

### Q8: What is HATEOAS?

**HATEOAS** (Hypermedia As The Engine Of Application State) is a REST constraint where responses include links to other resources, enabling discoverability.

#### Example:
```json
{
  "patientId": 100,
  "name": "John Doe",
  "_links": {
    "appointments": "/patients/100/appointments",
    "billing": "/patients/100/billing"
  }
}
