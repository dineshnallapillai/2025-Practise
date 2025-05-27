# System Design Interview Prep

---

## 1️⃣ How to Approach a System Design Interview

### 📌 Step-by-Step Method

1. **Clarify Requirements**
   - Functional: What does it do?
   - Non-functional: Scale? Latency? Uptime? Security?

2. **Estimate Scale**
   - Daily users, RPS, storage volume

3. **High-Level Components**
   - Define key layers: frontend, gateway, services, DB, storage, cache

4. **Drill Down Per Component**
   - Describe internal logic, APIs, storage format, scaling strategy

5. **Address Non-Functional Goals**
   - Availability, Scalability, Security, Monitoring

6. **Discuss Trade-offs**
   - Justify your tech choices (SQL vs NoSQL, REST vs events)

---

## 2️⃣ Requirements Template

### ✅ Functional
- Upload and view files
- Search metadata
- Real-time preview
- Admin management

### ✅ Non-Functional
- 99.9% uptime
- Scale to 1M users
- Upload size: up to 500MB
- < 300ms latency (P95)

---

## 3️⃣ Scenario: Design a Healthcare DICOM Imaging System

### 🧠 Functional Breakdown
- Upload large DICOM files (via Web UI or API)
- Store securely
- Retrieve via search (patient ID, modality, date)
- View using medical imaging viewer

---

## 4️⃣ High-Level Architecture

[Client] → [API Gateway] → [Auth Service] → [DICOM Upload Service]
↓
[Metadata DB] (PostgreSQL)
[Blob Store] (S3)
[Queue: Processing Jobs] → [Analysis Service]
[Viewer Service] ← [CDN/Image Proxy]

---


---

## 5️⃣ Key Components (Detailed)

### 🔹 API Gateway
- Routes incoming requests
- AuthN/AuthZ checks
- Handles rate limiting

### 🔹 Auth Service
- JWT/Cognito/SAML
- Role-based access: Radiologist, Technician

### 🔹 DICOM Upload Service
- Multipart upload support
- Validates file type, size, headers
- Writes to S3
- Pushes event to queue for async analysis

### 🔹 Metadata Database
- PostgreSQL (structured queries)
- Stores: patient ID, study UID, tags
- Indexed for fast lookup

### 🔹 Analysis Service (Optional)
- Event-driven via SQS or Kafka
- Runs AI/ML pipelines or anonymizes data
- Saves result metadata back to DB

### 🔹 Viewer Service
- Serves pre-rendered thumbnails
- Requests routed via CloudFront or ALB
- Caches in Redis or CDN

---

## 6️⃣ Scaling & Availability

### ✅ Uploads
- Use S3 multipart upload
- Pre-signed URLs for secure direct uploads

### ✅ Compute
- Auto-scale containers on ECS
- Use health checks and circuit breakers

### ✅ DB Layer
- Read replicas
- Sharded by facility/region if scale increases
- Backup + restore (point-in-time)

### ✅ Cache Layer
- Redis for recent image metadata
- CDN for static DICOM previews

---

## 7️⃣ Security, Auth, Rate Limiting

- HTTPS everywhere
- JWT tokens (Cognito / IdentityServer)
- Role-based access per API route
- WAF (Web Application Firewall)
- IP whitelisting (for hospital partners)
- AWS Secrets Manager for credentials
- Rate limit: 10 RPS/user, burst up to 50

---

## 8️⃣ Observability

### ✅ Logging
- Structured logs: Correlation ID, Trace ID
- Serilog + CloudWatch/Splunk

### ✅ Tracing
- OpenTelemetry + X-Ray
- Trace file upload → storage → database updates

### ✅ Metrics
- Requests/sec
- Queue depth
- Failed uploads
- Time to retrieve

---

## 9️⃣ CI/CD + Deployment

- Code pushed → GitHub Actions
- Build Docker image → push to ECR
- Deploy to ECS using blue/green (CodeDeploy)
- Rollback if health checks fail
- Infra as Code (CloudFormation or Terraform)

---

## 🔟 Trade-offs, Pitfalls & Interview Traps

### ❌ Anti-patterns to Avoid
| Mistake                          | Better Alternative                  |
|----------------------------------|--------------------------------------|
| Single DB for all services       | Per-service DB (bounded context)     |
| Sync service-to-service calls    | Use async + retries + backoff        |
| Storing files in DB              | Use S3, GCS, or Blob storage         |
| 1 big container                  | Split services into microservices    |

---

## ✅ Sample Interview Q&A

### Q1: How would you design a system to support 1M DICOM uploads/day?

**Answer:**
- Use S3 for massive file durability
- Asynchronous ingestion using SQS
- Stateless upload APIs for horizontal scaling
- CDN for repeat image access
- Redis + DB sharding for metadata lookup

---

### Q2: How do you ensure upload security?

- HTTPS with signed URLs
- Content-type filtering
- Virus scanning (Lambda / S3 trigger)
- Token-based upload auth

---

### Q3: How would you design a public REST API for external hospital systems?

- Expose via API Gateway
- Enforce OAuth2 / JWT
- Throttle via usage plans
- Log every access (compliance)

---

## 🧠 Interview Tips

- Always mention **scale assumptions**
- Prioritize **availability & data integrity**
- Speak in **layers**: network → compute → data → security
- Discuss **failure modes** and mitigation

---

## ✅ Summary

You should now be able to:
- Design multi-tier systems from scratch
- Communicate design clearly with trade-offs
- Pick cloud-native tools with reasoning
- Think like an architect: secure, observable, resilient

