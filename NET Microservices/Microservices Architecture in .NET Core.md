# Microservices Architecture in .NET Core

---

## 1️⃣ What is a Microservices Architecture?

Microservices are a software design pattern where the application is built as a **collection of loosely coupled, independently deployable services**.

---

### ✅ Key Characteristics

- Single Responsibility
- Autonomous (own DB, code, deployments)
- Independent scaling
- Decentralized governance
- Fault isolation

---

## 2️⃣ Domain-Driven Design (DDD) and Bounded Contexts

- DDD helps split the system into **Bounded Contexts**
- Each service should map to **one domain model**
- Use **Entities, Aggregates, Repositories**

### Example:
- `PatientService` → handles identity, demographics
- `ImagingService` → handles DICOM scans
- `BillingService` → handles invoices

---

## 3️⃣ Microservice Design Principles

### ✅ Decomposition Techniques

- **By Business Capability** – Recommended (e.g., Order, Payment, Shipping)
- **By Subdomain** – Using DDD (Core, Supporting, Generic)
- **Avoid** technical-layer splitting (e.g., Controllers, Services, DB)

---

### ✅ Ownership

- Each service owns:
  - **Code**
  - **Database**
  - **CI/CD Pipeline**
  - **Monitoring**

---

## 4️⃣ API Gateway (Ocelot / YARP)

API Gateway is the **entry point** for all clients.

### 🔹 Benefits

- Route requests to internal services
- Centralized AuthN/AuthZ
- Rate limiting
- Aggregation

### 🔹 Ocelot Example

```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/products",
      "UpstreamPathTemplate": "/products",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        { "Host": "localhost", "Port": 5001 }
      ]
    }
  ]
}

---


# ✅ YARP (Newer Gateway from Microsoft)

```csharp
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
```

---

## 5️⃣ Inter-Service Communication

### ✅ REST (Synchronous)

```csharp
var client = _httpClientFactory.CreateClient("users");
var user = await client.GetFromJsonAsync<UserDto>("api/users/1");
```

**Pros:**

- Simple
- Easy to debug

**Cons:**

- Tight coupling
- Hard to scale

### ✅ Message-Based (Asynchronous)

Use queues or topics (SQS, Kafka, RabbitMQ)

**Use Cases:**
- Decoupling services
- Resilience
- Event sourcing

---

## 6️⃣ Event-Driven Architecture (EDA)

Services emit events (e.g., "OrderPlaced") and others subscribe.

### ✅ Integration Patterns

| Pattern              | Example              |
|----------------------|----------------------|
| Pub/Sub              | Kafka, SNS-SQS       |
| Event-Carried State  | Full payload in message |
| Event Notification   | Only event name/id   |

---

## 7️⃣ Configuration & Service Discovery

### ✅ Configuration Options

- `.json` files
- Environment variables
- Consul / AWS Parameter Store / AppConfig

### ✅ Service Discovery

Use Consul, Eureka, or DNS-based discovery (ECS, Kubernetes)

---

## 8️⃣ Fault Tolerance

### ✅ Circuit Breaker (Polly)

```csharp
var policy = Policy.Handle<Exception>()
    .CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));
```

### ✅ Retry with Exponential Backoff

```csharp
var retry = Policy.Handle<Exception>()
    .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2 * i));
```

---

## 9️⃣ Observability: Logging, Tracing, Metrics

### ✅ Logging

- Use `ILogger<T>` or Serilog
- Include trace IDs, correlation IDs

### ✅ Distributed Tracing

- Use OpenTelemetry to trace across services
- Integrate with Jaeger, Zipkin, or AWS X-Ray

### ✅ Metrics

- Use Prometheus + Grafana
- Export metrics like request count, latency

---

## 🔟 Deployment & Hosting

### ✅ Containerization with Docker

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY . /app
WORKDIR /app
ENTRYPOINT ["dotnet", "MyService.dll"]
```

### ✅ Deploy to AWS ECS (Fargate)

- Use ECS Task Definitions
- Register containers
- Expose through ALB or API Gateway

---

## 🔸 Basic Interview Questions

**Q1: What are microservices?**  
Small, independently deployable services with clear boundaries.

**Q2: What is a bounded context?**  
A logical boundary for a domain where a model applies consistently (DDD concept).

**Q3: When should you use microservices?**  
When you need:
- Scalability
- Domain isolation
- Independent teams/releases

**Q4: What is an API gateway and why is it needed?**  
Single entry point for APIs, providing routing, auth, rate limiting, and aggregation.

---

## 🔸 Advanced Interview Questions

**Q5: How do you implement service-to-service security?**  
- Use JWT between services  
- Mutual TLS (mTLS)  
- API keys or signed headers

**Q6: How do you handle failure in microservices?**  
- Use retries, circuit breakers  
- Timeouts and fallback logic  
- Queue-based decoupling

**Q7: How do you manage schema evolution in events?**  
- Use versioned events  
- Keep backward compatibility  
- Avoid breaking changes

**Q8: How would you test microservices end-to-end?**  
- Use contract testing (e.g., Pact)  
- Deploy to test environments with real integrations  
- Use Docker Compose or Kubernetes

---

## ✅ Summary

- Microservices should be small, loosely coupled, and independently deployable.
- Use DDD for clear boundaries.
- Handle communication with REST or events.
- Add observability, retries, and monitoring for resilience.
- Deploy with containers using ECS, Kubernetes, or App Services.
