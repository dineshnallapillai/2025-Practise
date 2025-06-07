# 🌐 AWS High Availability & Disaster Recovery (HA/DR) – Complete Guide

## 🔸 Key Concepts

| Term              | Description                                                     |
|-------------------|-----------------------------------------------------------------|
| High Availability (HA) | Architecting for minimal downtime due to failures (zone/instance) |
| Disaster Recovery (DR) | Strategy to recover from catastrophic events (region-level failure) |
| Multi-AZ          | Spread resources across Availability Zones (within a region)    |
| Multi-Region      | Replicate resources to another AWS region                       |
| RTO               | Recovery Time Objective → how fast to recover                   |
| RPO               | Recovery Point Objective → how much data loss is acceptable     |

---

## 🔹 Availability Zones vs Regions

| Term              | Scope                        | Use For                             |
|-------------------|------------------------------|--------------------------------------|
| Availability Zone (AZ) | Data center within a Region   | Fault isolation, low latency failover |
| Region            | Geographic area (e.g., us-east-1) | Compliance, disaster isolation        |

---

## 🔹 AWS Built-in HA Capabilities

| Service                   | Built-in HA? | Notes                                                  |
|---------------------------|--------------|--------------------------------------------------------|
| S3                        | ✅ Yes       | Multi-AZ, 11 9’s durability (99.999999999%)            |
| RDS Multi-AZ              | ✅ Yes       | Standby Replica, Auto-failover between AZs            |
| Aurora                    | ✅ Yes       | 6 copies across 3 AZs, highly fault-tolerant          |
| DynamoDB                  | ✅ Yes       | Multi-AZ, Global Tables for multi-region              |
| Elastic Load Balancer     | ✅ Yes       | Auto traffic distribution across AZs                  |
| ECS/Fargate               | ✅ Yes*      | Must run tasks in multiple AZs                        |
| Lambda                    | ✅ Yes       | Region-wide fault tolerance and scalability           |
| Route 53                  | ✅ Yes       | Global DNS, health checks, geo-routing                |
| S3 Glacier Deep Archive   | ❌ No        | Long restore times, good for cold backup only         |

---

## 🔹 Multi-AZ vs Multi-Region – Comparison

| Feature          | Multi-AZ                       | Multi-Region                                |
|------------------|-------------------------------|---------------------------------------------|
| Scope            | Fault isolation within region | Disaster isolation across continents        |
| Latency          | Low (milliseconds)            | High (100+ milliseconds)                    |
| Data Sync        | Synchronous (e.g., RDS)       | Asynchronous (e.g., S3 CRR)                 |
| Complexity       | Low                           | High (duplicate infra, DNS, auth, etc.)     |
| Use Cases        | HA, local failover            | DR, compliance, geo-proximity, legal        |

---

## 🔹 4 AWS DR Strategies (Based on AWS Whitepaper)

| DR Strategy         | RTO       | RPO       | Cost     | Description                                                    |
|---------------------|-----------|-----------|----------|----------------------------------------------------------------|
| Backup & Restore    | Hours     | Hours     | 💲        | Store backups in S3 or Glacier. Restore when disaster occurs.  |
| Pilot Light         | 10–30 min | Minutes   | 💲💲      | Core infra running (DBs), rest launched on-demand.             |
| Warm Standby        | <10 min   | <1 min    | 💲💲💲    | Scaled-down copy of prod running in DR region.                 |
| Multi-Site (Active-Active) | ~0        | ~0        | 💲💲💲💲 | Fully active infra in multiple regions.                        |

---

## ✅ DR Strategy Comparison Table

| Strategy          | RTO     | RPO     | Cost     | Example Tools                                                   |
|-------------------|---------|---------|----------|------------------------------------------------------------------|
| Backup & Restore  | Hours   | Hours   | 💲        | AWS Backup, S3, Glacier, RDS snapshots                          |
| Pilot Light       | 10–30m  | Minutes | 💲💲      | CloudFormation, AMIs, Route 53                                  |
| Warm Standby      | <10m    | <1m     | 💲💲💲    | Cross-region replication, RDS read replicas                     |
| Active-Active     | ~0      | ~0      | 💲💲💲💲 | Global DynamoDB, Route 53, Multi-region API Gateway, ELB        |

---

## 🔹 Common HA/DR Services & Features

| AWS Service                | HA/DR Feature                                      |
|----------------------------|---------------------------------------------------|
| Route 53                   | DNS failover, latency routing, geo routing       |
| Elastic Load Balancer      | Multi-AZ load balancing                          |
| RDS Multi-AZ               | Sync standby instance in another AZ              |
| Aurora Global Database     | Cross-region DB replication                      |
| DynamoDB Global Tables     | Multi-region data sync                           |
| S3 Cross-Region Replication| For DR or compliance copies                      |
| CloudFront                 | Edge cache in 300+ locations                     |
| CloudFormation             | Re-create infra in DR region                     |
| AWS Backup                 | Centralized backup management                    |
| AWS Elastic Disaster Recovery (DRS) | Lift & shift + failover EC2/VMS to AWS        |

---
# 🚀 AWS High Availability & Disaster Recovery – Pro Tips & Interview Q&A

## ✅ Pro Tips

- **✅ Use Route 53 failover + health checks** for DNS-level High Availability
- **✅ Replicate S3, RDS, and DynamoDB across regions** for critical workloads
- **✅ Use Multi-AZ deployments** for handling short-term infrastructure failures
- **✅ Periodically test DR readiness** via simulations and recovery drills
- **✅ Automate infrastructure recreation** using AWS CloudFormation or CDK
- **✅ Use Aurora Global DB** for cross-region, low-latency database replication and HA

---

## ❓ What’s the difference between Multi-AZ and Multi-Region?

| Feature          | Multi-AZ                                 | Multi-Region                                      |
|------------------|------------------------------------------|---------------------------------------------------|
| Scope            | Within a single AWS Region               | Across multiple AWS Regions                       |
| Purpose          | High availability, fault tolerance       | Disaster recovery, compliance, latency optimization |
| Data Sync        | Synchronous (e.g., RDS standby)          | Asynchronous (e.g., S3 CRR)                       |
| Latency          | Low (ms)                                 | Higher (100+ ms)                                  |
| Complexity       | Low                                       | High – requires duplicate infra, DNS failover    |

---

## ❓ How do you achieve HA for a stateless app running in EC2?

✅ Recommended architecture:
- Deploy EC2 instances in **multiple AZs**
- Place instances behind an **Elastic Load Balancer (ELB)**
- Use **Auto Scaling Groups (ASG)** to maintain instance count
- Use **Route 53 health checks** to reroute traffic if an AZ fails
- Store session state in a shared service like **DynamoDB**, **ElastiCache**, or **S3**

---

## ❓ How do you architect for RTO < 1 min and RPO ~0?

Choose the **Multi-Site (Active-Active)** DR strategy:
- ✅ Deploy production workloads in **multiple regions**
- ✅ Use **Route 53 latency/geo-based routing + failover**
- ✅ Enable **cross-region replication** (Aurora Global DB, DynamoDB Global Tables, S3 CRR)
- ✅ Ensure **stateless components are synchronized** (e.g., infrastructure-as-code, CI/CD pipelines)
- ✅ Use **Global Load Balancing** and health checks for real-time failover

---

## ❓ Explain the 4 DR models AWS recommends.

| DR Strategy        | RTO        | RPO        | Description                                                 |
|--------------------|------------|------------|-------------------------------------------------------------|
| **Backup & Restore** | Hours      | Hours      | Store backups in S3/Glacier, restore on disaster            |
| **Pilot Light**      | 10–30 mins | Minutes    | Keep critical systems (DBs) running; rest spun up on demand |
| **Warm Standby**     | <10 mins   | <1 min     | Scaled-down prod environment always running                 |
| **Multi-Site**       | ~0         | ~0         | Fully active, replicated systems in multiple regions        |

---

## ❓ How would you fail over Route 53 in case of region failure?

1. Use **Route 53 health checks** to monitor endpoints (e.g., ELB or custom health check Lambda)
2. Set up **Failover Routing Policy**:
   - **Primary record** points to active region
   - **Secondary record** points to standby region
3. If primary endpoint fails health check, Route 53 routes traffic to the standby region

---

## ❓ Can S3 help you with cross-region DR?

✅ **Yes**. Use **S3 Cross-Region Replication (CRR)**:
- Automatically replicates S3 objects to another region
- Maintains **RPO close to zero**
- Helps in disaster recovery, compliance, and geo-locality
- Works with versioning, encryption, and IAM policies

---

