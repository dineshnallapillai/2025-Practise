
# Microservices Architecture Patterns

---

## 1. Decomposition Patterns

| Pattern                        | Purpose                                  | Example                                    |
|-------------------------------|------------------------------------------|--------------------------------------------|
| Decompose by Business Capability | Split services around business functions | Inventory, Billing, Shipping               |
| Decompose by Subdomain        | Align with Domain-Driven Design subdomains | Core, Supporting, Generic subdomains       |
| Self-Contained Services (SCS) | Each service includes its own UI, logic, and DB | A reporting module with its own stack |

---

## 2. Integration Patterns

| Pattern                | Purpose                             | Example                                              |
|------------------------|-------------------------------------|------------------------------------------------------|
| API Gateway            | Single entry point for clients      | Netflix Zuul, AWS API Gateway                        |
| Aggregator             | Collect data from multiple services | Order service calling Inventory and Pricing          |
| Backend for Frontend (BFF) | Custom gateway per client type | One BFF for mobile, one for web                      |
| Event-Driven Communication | Async event-based interaction   | RabbitMQ or Kafka for Order → Payment →             |

---

## 3. Database Patterns

| Pattern              | Purpose                               | Example                                             |
|----------------------|---------------------------------------|-----------------------------------------------------|
| Database per Service | Each service manages its own schema   | Product service → Product DB                        |
| Shared Database      | Multiple services use the same DB (anti-pattern unless legacy) | User and Orders use same DB      |
| Saga Pattern         | Manage distributed transactions       | Hotel Booking → Payment → Car rental                |
| CQRS                 | Separate read and write models        | Write to event store, read from SQL DB              |

---

## 4. Observability Patterns

| Pattern           | Purpose                              | Example                          |
|-------------------|--------------------------------------|----------------------------------|
| Log Aggregation   | Central log collection               | ELK stack, Fluentd               |
| Distributed Tracing | Track requests across services      | Zipkin, Jaeger                   |
| Health Check API  | Endpoint for health status           | /health returns 200 OK           |
| Metrics Collection | Gather service metrics              | Prometheus, Grafana              |

---

## 5. Cross-Cutting Concern Patterns

| Pattern                | Purpose                                | Example                             |
|------------------------|----------------------------------------|-------------------------------------|
| Externalized Configuration | Load config from external sources | Spring Cloud Config                 |
| Service Discovery      | Dynamic endpoint resolution            | Eureka, Consul                      |
| Circuit Breaker        | Prevent cascading failure              | Resilience4j, Hystrix               |
| API Rate Limiting      | Prevent abuse of APIs                  | Token bucket algorithm              |

---

## 6. Reliability Patterns

| Pattern         | Purpose                           | Example                               |
|------------------|-----------------------------------|---------------------------------------|
| Retry           | Retry failed requests              | Retry DB call 3 times                 |
| Timeout         | Abort slow operations              | 2 sec timeout for HTTP call           |
| Circuit Breaker | Open circuit after N failures      | Avoid overloading service             |
| Failover        | Switch to backup service           | Secondary DB instance                 |

---

## 7. Security Patterns

| Pattern                | Purpose                              | Example                             |
|------------------------|--------------------------------------|-------------------------------------|
| Access Token           | Token-based authentication           | JWT, OAuth2 tokens                  |
| HTTPS Everywhere       | Encrypt traffic                      | TLS for all communication           |
| OAuth2/OpenID Connect  | Delegated auth                       | Auth0, Google login                 |
| Service-to-Service Auth| Secure internal calls                | mTLS, SPIFFE                        |

---

## 8. Scalability & Isolation Patterns

| Pattern                | Purpose                              | Example                             |
|------------------------|--------------------------------------|-------------------------------------|
| Bulkhead              | Isolate resources per service/thread pool | Prevents DB connection exhaustion |
| Queue-Based Load Leveling | Handle spikes asynchronously     | Job queues (e.g., SQS)              |
| Throttling             | Limit resource usage                 | Max 100 calls/sec                   |
| Rate Limiting          | Enforce usage quota                  | IP-based limiter                    |

---

## 9. Deployment & Evolution Patterns

| Pattern               | Purpose                               | Example                             |
|------------------------|---------------------------------------|-------------------------------------|
| Strangler Fig         | Gradually replace legacy              | Move one module at a time to microservices |
| Sidecar               | Attach helper services to a container | Envoy for traffic control           |
| Ambassador            | Proxy for remote service access       | Ambassador pattern with API Gateway |
| Blue-Green Deployment | Switch between old/new environments   | Route traffic from v1 to v2         |
| Canary Release        | Release to small user group first     | 5% traffic to new version           |
| Service Mesh          | Manage microservice comms with sidecars | Istio, Linkerd                     |
