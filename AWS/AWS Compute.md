# AWS Compute Services Overview

Compute services are at the core of running workloads in the cloud. AWS offers various compute models for different needs — virtual machines, containers, serverless, and more.

---

## ✅ 1. Amazon EC2 (Elastic Compute Cloud)

**What is EC2?**  
A virtual server (VM) in AWS cloud where you can run OS-level workloads (Linux, Windows, etc.)

### ⚙️ EC2 Core Concepts

| Concept             | Description                                               |
|---------------------|-----------------------------------------------------------|
| AMI (Amazon Machine Image) | OS template (Ubuntu, Amazon Linux, Windows)          |
| Instance Type       | CPU, Memory optimized (e.g., t3.micro, m5.large)           |
| Instance Lifecycle  | Start → Stop → Reboot → Terminate                          |
| EBS (Elastic Block Store) | Persistent volumes for EC2                             |
| ENI (Elastic Network Interface) | Network adapter (attach/detach from EC2)          |
| Elastic IP          | Static public IP for EC2                                   |
| Placement Groups    | Cluster, Spread, Partition (control AZ placement)          |
| EC2 Hibernation     | Save RAM + disk state                                      |
| Host vs Instance    | Bare metal (Dedicated Host) vs virtual instance            |

### 📌 EC2 Purchasing Options

| Type               | Description                | Cost    | Use Case                        |
|--------------------|----------------------------|---------|--------------------------------|
| On-Demand          | Pay-per-hour/second         | High    | Dev/Test                       |
| Reserved           | Commit for 1–3 years        | Low     | Predictable workloads          |
| Spot               | Bid for unused capacity     | Very Low| Batch/CI/Non-critical          |
| Savings Plans      | Flexible commitment         | Medium  | Mixed workload optimization    |
| Dedicated Host/Instance | Physical isolation      | High    | Licensing/Compliance needs     |

### 🛡️ EC2 Security Best Practices

- Use **Security Groups** (stateful) and **NACLs** (stateless)  
- Use **IAM roles** instead of hardcoded credentials  
- Enable **CloudWatch Logs** and **CloudTrail**  
- Use **SSM Session Manager** to access instances without SSH keys  

---

## ✅ 2. Auto Scaling & Elastic Load Balancing

### 🔄 Auto Scaling Groups (ASG)

Automatically adjust EC2 capacity based on demand.

| Feature            | Description                                |
|--------------------|--------------------------------------------|
| Launch Template    | Defines EC2 configuration                    |
| Min/Max/Desired Capacity | Controls scaling parameters             |
| Scaling Policies   | Based on CPU %, custom metrics, step scaling |
| Health Checks      | Replace unhealthy instances                  |

**Benefits:** Resilience, cost-efficiency, elasticity.

### ⚖️ Elastic Load Balancer (ELB)

Distributes incoming traffic across multiple targets.

| Type               | Description                                  |
|--------------------|----------------------------------------------|
| ALB (Application Load Balancer) | Layer 7 (HTTP/HTTPS), Path/Host-based routing |
| NLB (Network Load Balancer)     | Layer 4 (TCP/UDP), ultra-high performance      |
| CLB (Classic Load Balancer)     | Legacy, only use for migration                   |

Combine **ASG + ELB** for fault-tolerant web applications.

---

## ✅ 3. AWS Lambda – Serverless Compute

**What is Lambda?**  
Run code without managing servers. Pay only for execution duration and invocations.

### Core Features:

- Supports many languages (Node.js, Python, Java, .NET, etc.)  
- Event-driven (S3, SNS, API Gateway, etc.)  
- Automatic scaling  
- Max execution time: 15 minutes  
- Granular billing (per millisecond)  
- Secure by default (VPC, IAM roles)  

### Common Use Cases:

- Backend APIs  
- File processing pipelines (e.g., S3 → Lambda → DynamoDB)  
- Event-driven automation  
- Infrastructure automation (EventBridge triggers)  

---

## ✅ 4. ECS & AWS Fargate

### ➤ ECS (Elastic Container Service)

AWS-managed container orchestration for Docker containers.

| Mode             | Description                     |
|------------------|---------------------------------|
| EC2 Launch Type  | You manage the EC2 infrastructure |
| Fargate          | AWS manages infrastructure, you manage containers |

Works with ALB for service discovery, integrates natively with IAM and CloudWatch.

### ➤ Fargate

Serverless container engine — no need to manage EC2 instances.

Define task → Deploy → AWS provisions CPU & RAM automatically.

---

## ✅ 5. EKS (Elastic Kubernetes Service)

Fully managed Kubernetes on AWS.

- Use `kubectl` to manage workloads  
- Supports hybrid on-prem + cloud workloads  
- Integrates with AWS services (ALB ingress, IAM Roles for Service Accounts, etc.)  
- Best choice if already using Kubernetes  

---

## ✅ 6. AWS Lightsail

Simplified compute service with UI-based provisioning.

- Great for simple web apps, blogs, testing  
- Bundled VM, storage, static IP, networking  
- Less flexible than EC2, but beginner-friendly  

---
# AWS Compute Concepts: FAQs

---

### 1. Difference between ECS EC2 Launch Type and Fargate

| Feature               | ECS EC2 Launch Type                      | Fargate                                   |
|-----------------------|----------------------------------------|-------------------------------------------|
| Infrastructure        | You manage the EC2 instances            | AWS manages the infrastructure             |
| Server Management     | You are responsible for patching, scaling, and capacity | Fully serverless, no server management needed |
| Pricing               | Pay for EC2 instances regardless of usage | Pay only for resources used by containers   |
| Use Case              | More control over instance types and networking | Simplified container deployment with less operational overhead |
| Scaling               | You scale EC2 capacity manually or via ASG | Automatic scaling of compute resources     |

---

### 2. How does Lambda scale?

- Lambda automatically scales **concurrently** by running multiple instances of your function in response to incoming events.  
- There is no need to provision servers; AWS spins up new function instances to handle increased load.  
- The scaling is near-instant and managed by AWS.  
- The maximum concurrency limit can be configured or requested to increase.

---

### 3. How do you make EC2 highly available?

- **Deploy instances across multiple Availability Zones (AZs)** using Auto Scaling Groups (ASGs).  
- Use **Elastic Load Balancers (ALB/NLB)** to distribute traffic across healthy instances.  
- Implement **health checks** to replace unhealthy instances automatically.  
- Use **Placement Groups** (e.g., spread or partition) to reduce correlated failures.  
- Utilize **multi-region replication** for disaster recovery (optional).

---

### 4. Difference between ALB and NLB

| Feature                  | ALB (Application Load Balancer)                      | NLB (Network Load Balancer)                    |
|--------------------------|------------------------------------------------------|------------------------------------------------|
| OSI Layer                | Layer 7 (Application Layer)                          | Layer 4 (Transport Layer)                       |
| Protocols                | HTTP, HTTPS                                          | TCP, UDP, TLS                                   |
| Routing                 | Supports path-based, host-based routing               | Pass-through routing, very low latency         |
| Use Cases                | Web applications, microservices                       | High-performance, TCP/UDP load balancing        |
| SSL Termination          | Supported                                            | Supported                                       |
| IP Address Type          | Dynamic, supports IP-based routing                    | Static IP support with Elastic IPs              |

---

### 5. When do you use Spot Instances?

- When you want to **reduce costs** significantly (up to 90% cheaper than On-Demand).  
- Suitable for **fault-tolerant, flexible workloads** like:  
  - Batch processing  
  - Big data analytics  
  - CI/CD pipelines  
  - Development/testing environments  
  - Stateless or easily recoverable jobs  

**Avoid** for critical production workloads unless combined with fault-tolerant architecture (e.g., ASG mixed instances).

---

### 6. How would you set up blue/green deployments using Lambda or ECS?

#### Lambda

- Use **AWS Lambda Aliases** and **Traffic Shifting**:  
  - Deploy new version of Lambda function.  
  - Use alias to point to the current version.  
  - Shift traffic gradually from old version to new version (canary deployments).  
  - Monitor and rollback if issues occur.

#### ECS

- Use **ECS Blue/Green Deployment with AWS CodeDeploy**:  
  - Create two separate task sets (blue = current, green = new).  
  - Deploy the new task set (green).  
  - Shift traffic gradually from blue to green using Application Load Balancer.  
  - Rollback by switching traffic back to blue if needed.

Both methods allow **zero-downtime deployments** and easy rollback.

---
