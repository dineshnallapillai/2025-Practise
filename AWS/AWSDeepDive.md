# 5 – AWS  (.NET + Cloud Architecture)

---

## 1️⃣ IAM (Identity & Access Management)

### 🔹 Core Concepts

| Entity      | Use Case                             |
|-------------|--------------------------------------|
| User        | Human admin access                   |
| Group       | Permission grouping for users        |
| Role        | Temporary credentials for services   |
| Policy      | JSON-based permission documents      |

### ✅ Least Privilege Best Practice
Grant only the minimum permissions needed.

### ✅ Example IAM Policy

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": "s3:PutObject",
      "Resource": "arn:aws:s3:::medical-dicom-bucket/*"
    }
  ]
}
```
---

## 2️⃣ Compute: ECS vs Lambda vs EC2

### ✅ ECS (Elastic Container Service)
- Best for containerized .NET APIs
- Works with Fargate (serverless containers)
- Auto scaling + integrated with ALB

```bash
docker build -t dicom-api .
aws ecr create-repository ...
aws ecs create-service ...
```

### ✅ AWS Lambda
- Best for event-driven, short-lived functions
- Cold starts still apply, but .NET 8 with AOT helps
- Useful for:
  - S3 triggers
  - Queue processing
  - Lightweight webhooks

### ✅ EC2 (Elastic Compute Cloud)
- Full VM control
- Use sparingly — higher ops overhead
- Use Auto Scaling Groups for resilience

## 3️⃣ Storage Options

| Storage | Use Case |
|--------|----------|
| S3     | Unstructured object storage (DICOM) |
| EBS    | Block storage for EC2/ECS |
| EFS    | Shared file system (multiple EC2s) |
| Backup | Scheduled snapshots (automated) |

### ✅ S3 Lifecycle Rules
- Archive to Glacier after 30 days
- Delete after 365 days
- Encrypt at rest (AES-256 or KMS)

## 4️⃣ Networking (VPC Deep Dive)

### ✅ VPC Components

| Component        | Purpose                              |
|------------------|--------------------------------------|
| CIDR Block       | IP range for VPC (e.g. 10.0.0.0/16)  |
| Subnets          | Divide VPC into AZs (Public/Private) |
| Route Tables     | Define traffic rules                 |
| NAT Gateway      | Allow outbound internet from private subnets |
| Internet Gateway | Enable public subnet internet access |
| Security Groups  | Virtual firewalls for EC2, ECS       |

### ✅ Best Practices
- Use private subnets for DBs/services
- Public subnet only for Load Balancer
- Separate Dev, Staging, Prod VPCs
- Allow only required ports (e.g. 443, 5432)

## 5️⃣ Load Balancers & Auto Scaling

### ✅ Types of Load Balancers

| Type | Use For |
|------|---------|
| ALB  | HTTP/HTTPS routing (API traffic) |
| NLB  | TCP/UDP at low latency |

### ✅ Auto Scaling Groups
- Trigger on metrics (CPU, request count)
- Minimum and maximum capacity limits
- Useful with EC2 or ECS with EC2 launch type

## 6️⃣ Databases

### ✅ RDS (SQL)
- PostgreSQL, SQL Server, MySQL
- Multi-AZ failover
- Automated backups + snapshots
- Parameter groups for fine tuning

```sql
CREATE TABLE dicom_metadata (
  id UUID PRIMARY KEY,
  patient_id TEXT,
  modality TEXT,
  file_url TEXT
);
```

### ✅ Aurora
- AWS-native RDS engine (PostgreSQL/MySQL compatible)
- Auto-scaling storage
- 5x faster read performance

### ✅ DynamoDB (NoSQL)
- Key-value + document data store
- Scales horizontally
- TTL, streams, transactions

## 7️⃣ Messaging & Eventing

| Service    | Use Case                      |
|------------|-------------------------------|
| SQS        | Queuing + decoupling          |
| SNS        | Fan-out pub/sub               |
| EventBridge| Event router (rule-based dispatch) |
| MSK (Kafka)| High-throughput messaging (optional) |

### ✅ Example: Upload Event Flow
1. S3 upload triggers Lambda
2. Lambda sends event to SQS
3. Processing service reads from SQS and writes to DB

## 8️⃣ Monitoring & Observability

### ✅ CloudWatch
- Metrics (CPU, memory, logs)
- Alarms → SNS/Email
- Log groups per ECS task / Lambda function

```csharp
logger.LogInformation("User uploaded DICOM {id}", id);
```

### ✅ X-Ray
- Distributed tracing across services
- View call durations, bottlenecks
- Integrates with .NET using SDK

### ✅ OpenTelemetry (Advanced)
- Vendor-neutral
- Traces, metrics, logs
- Works with Jaeger, Prometheus

## 9️⃣ CI/CD with GitHub Actions + AWS

### ✅ Typical Workflow
- Push to GitHub triggers CI
- Build .NET app + Docker image
- Push to ECR
- Deploy via ECS or CodeDeploy

### ✅ GitHub Action Sample

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0.x'
      - run: dotnet publish -c Release
      - run: docker build -t myapp .
      - run: aws ecr push ...
```

## 🔟 Infrastructure as Code

| Tool           | Pros                             |
|----------------|----------------------------------|
| CloudFormation | Native to AWS, declarative       |
| AWS CDK        | Imperative, typesafe (C#/TypeScript) |
| Terraform      | Multi-cloud, mature ecosystem    |

### ✅ Example CDK Stack (C#)

```csharp
var bucket = new Bucket(this, "DICOMBucket", new BucketProps
{
    Versioned = true,
    RemovalPolicy = RemovalPolicy.DESTROY
});
```

## ✅ Interview Questions

### Q1: What are the pros/cons of ECS vs Lambda?

| ECS             | Lambda                       |
|------------------|-------------------------------|
| Long-running apps| Short-lived event handlers    |
| Better for APIs  | Better for queues, S3         |
| Warm container reuse | Cold start for .NET     |
| More infra control | Minimal ops effort         |

### Q2: How do you secure your workloads?
- IAM least privilege
- S3 Bucket policies + encryption
- Private subnets for ECS/RDS
- Secrets in AWS Secrets Manager
- JWT auth with API Gateway
- WAF + rate limiting

### Q3: How do you scale in AWS?
- Auto Scaling Groups (EC2/ECS)
- Lambda concurrency
- Aurora + read replicas
- CloudFront for static files
- SQS for queue buffer
- Cache using ElastiCache (Redis)

## ✅ Summary
You now know:
- How to deploy and scale .NET services on AWS
- How to decouple and monitor your system
- When to use ECS, Lambda, SQS, RDS, DynamoDB
- How to build a secure, observable, CI/CD-enabled platform
