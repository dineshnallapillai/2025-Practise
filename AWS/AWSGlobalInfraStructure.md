# AWS Global Infrastructure

The AWS Global Infrastructure is the physical and logical foundation of all AWS services and architecture. You must understand how it's designed before anything else.

---

## ✅ 1. Regions

### ➤ What is a Region?

A **Region** is a geographical area with **2 or more Availability Zones (AZs)**.  
Each Region is **isolated** and fully independent to provide **fault tolerance and stability**.

### ➤ Examples

- `us-east-1` → N. Virginia  
- `ap-south-1` → Mumbai  
- `eu-west-1` → Ireland

### ➤ Key Points

| Topic     | Details                                                       |
|-----------|---------------------------------------------------------------|
| Isolation | Each region is isolated to contain failure impact            |
| Latency   | Choose a region close to users                               |
| Compliance| Data residency & regulatory reasons (e.g., GDPR)             |
| Cost      | Prices vary between regions                                  |
| Services  | Some services are region-specific, others are global (e.g., IAM, Route 53, CloudFront) |

### ➤ Interview Pitfall

> **“Can I move an AMI from `us-east-1` to `ap-south-1`?”**

❌ **No, not directly.** You must **copy** it to another region.

---

## ✅ 2. Availability Zones (AZs)

### ➤ What is an AZ?

An **Availability Zone** is one or more **discrete data centers** with **redundant power, networking, and connectivity**.  
AZs in a Region are **physically separated** by many kilometers.

### ➤ Why AZs Matter?

- Deploying across AZs = **High Availability** + **Fault Tolerance**
- If one AZ fails (e.g., power outage), your system stays operational via another AZ.

### ➤ Key Architecting Practice

- Use **Auto Scaling Groups** + **Load Balancer** across **multiple AZs**

---

## ✅ 3. Edge Locations

### ➤ What is an Edge Location?

These are **data centers** used by:
- **CloudFront (CDN)**
- **Route 53**
- **Lambda@Edge**
- **AWS Shield**

Designed to **cache content** and **reduce latency** for end users globally.

### ➤ Difference from Region/AZ

| Type           | Purpose                              |
|----------------|--------------------------------------|
| Region         | Where you run your applications      |
| AZ             | Physical datacenters inside a Region |
| Edge Location  | CDN endpoints to deliver static/cached content |

---

## ✅ 4. Local Zones

### ➤ What are Local Zones?

AWS **Local Zones** are an **extension of a Region** that places **compute, storage, database**, and other services **closer to population/industry centers**.

**Example**: For ultra-low latency gaming or video rendering in LA, there's a **Local Zone in Los Angeles** (associated with `us-west-2`).

---

## ✅ 5. Wavelength Zones

AWS infrastructure **deployed at telecom edge (5G)** to reduce latency for **mobile applications**.

**Use-cases**:
- AR/VR
- ML inference
- Real-time mobile applications

---

## ✅ 6. AWS Outposts

**Physical servers** installed **on-premises**, but **fully managed by AWS**.

**Use when**:
- You need **low latency** to on-prem systems
- You want **local data processing** with AWS services

---

## 🔁 Summary Table: Global Infrastructure Types

| Type             | Description                      | Use Case Example                              |
|------------------|----------------------------------|-----------------------------------------------|
| Region           | Geo location with AZs            | Choose for compliance, latency                |
| Availability Zone| Isolated DCs in Region           | Deploy multi-AZ for HA                        |
| Edge Location    | CDN cache and DNS endpoints      | Use with CloudFront, Route 53                 |
| Local Zone       | Compute/storage near large cities| Ultra-low latency gaming, streaming           |
| Wavelength Zone  | Edge for 5G mobile latency       | AR, VR, live video streaming                  |
| Outposts         | AWS on-prem fully managed        | Hybrid scenarios, regulated environments      |

---

## 📌 Whitepaper Insight

> “Regions are designed to be completely isolated from each other. AZs within a Region are interconnected with high-bandwidth, low-latency networking, but still physically isolated to prevent single points of failure.”

---

# Understanding AWS Global Infrastructure Components

---

## 🔁 Difference Between an AZ and a Local Zone

### **Availability Zone (AZ):**

- One or more **discrete data centers** within an AWS Region.
- Each AZ has **independent power, cooling, networking**, and **physical security**.
- AZs are **isolated** from failures in other AZs.
- AZs are interconnected with **high-bandwidth, low-latency fiber optic networks**.
- Designed for **high availability** and **fault tolerance**.
- If one AZ fails, applications can continue running in other AZs within the same Region.

### **Local Zone:**

- An **extension of an AWS Region**, placing services closer to **large population centers** and **industry hubs**.
- Enables **single-digit millisecond latency** to end-users.
- Connected to the parent Region via **secure, high-bandwidth** connections.
- Best suited for:
  - **Ultra-low latency** workloads
  - **Data residency** constraints
- **Not** focused on fault tolerance across a Region.

### ✅ Summary:

> **AZs** are for **high availability** within a Region, while **Local Zones** extend AWS infrastructure to specific cities for **low latency** access.

---

## ✅ How to Achieve High Availability in AWS Using AZs

You achieve **High Availability (HA)** in AWS by **distributing application resources** across multiple AZs.

### ✅ Key Concepts:

- **Fault Isolation**: AZs are isolated; failures in one don't affect others.
- **Redundancy**: Deploy components (e.g., EC2, DBs) across multiple AZs.

### ✅ Tools and Services:

- **Elastic Load Balancing (ELB)**:
  - Distributes traffic across AZs.
  - Automatically routes around failures.

- **Auto Scaling**:
  - Launches instances in healthy AZs.
  - Maintains desired instance count.

- **Amazon RDS (Multi-AZ)**:
  - Automatically provisions **synchronous standby** replicas.
  - Fails over to standby in case of failure.

- **Amazon S3**:
  - Automatically replicates data across multiple AZs.

### ✅ Example Architecture:

- Web servers in:
  - `us-east-1a`
  - `us-east-1b`
  - `us-east-1c`
- Behind an **Application Load Balancer** for traffic distribution and failover.

---

## ❓ Can I Use Services Like Lambda in Local Zones?

- **No**, AWS Lambda is **not currently supported** directly in Local Zones.
- Local Zones support a **subset of services**:
  - EC2
  - EBS
  - FSx
  - VPC
  - ELB
  - ECS / EKS

### ✅ Workaround:

- Run **latency-sensitive** workloads in the Local Zone.
- Invoke **Lambda** in the **parent Region** for backend processing.

---

## 🚀 Use of Wavelength Zones in 5G Applications

### 🔍 What Are Wavelength Zones?

- AWS compute/storage deployed **inside telecom providers' 5G networks**.
- Built for **ultra-low latency** over 5G.

### ✅ Benefits:

- **Single-digit millisecond latency**.
- Application traffic doesn't leave the telco network.
- Ideal for mobile-first, real-time use cases.

### 🔧 Use Cases:

- 🎮 Real-time gaming
- 📹 Live video streaming
- 🕶️ AR/VR applications
- 🚗 Autonomous vehicles
- 🏭 Industrial automation
- 🤖 Edge-based ML inference

---

## 🌍 How to Reduce Content Latency for Remote Users

### ✅ 1. **Amazon CloudFront (CDN)**

- **Edge Locations** globally placed for low-latency delivery.
- **Caching** static content (images, JS, videos).
- **TCP/TLS Termination** at the edge.

### ✅ 2. **AWS Global Accelerator**

- Uses AWS **global network** to optimize traffic routing.
- **Anycast IPs** for nearest edge routing.
- Reduces **jitter**, **packet loss**, and **latency**.

### ✅ 3. **AWS Local Zones**

- Deploy latency-sensitive workloads **close to urban hubs**.
- Reduces latency for city-specific users.

### ✅ 4. **AWS Wavelength Zones** (for 5G users)

- Brings compute directly **into 5G networks**.
- Enables ultra-low latency for mobile applications.

### ✅ 5. **Multi-Region Deployment**

- Deploy workloads in **multiple Regions**.
- Use **Amazon Route 53** with **latency-based routing**.
- Routes traffic to nearest healthy Region.

### ✅ 6. **AWS Direct Connect** (for hybrid)

- Dedicated, private connection to AWS.
- Reduces latency between on-prem and AWS.

---

By combining these services, you can architect globally distributed, low-latency applications tailored to user needs across the world.
