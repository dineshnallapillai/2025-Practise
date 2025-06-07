# AWS Storage Services – In-Depth Guide

AWS offers a broad range of storage solutions optimized for object, file, and block storage, suitable for all kinds of workloads from static websites to high-performance databases.

---

## 🔷 1. Amazon S3 (Simple Storage Service)

### ✅ What is S3?  
Object storage service built to store and retrieve any amount of data. Highly scalable, durable (99.999999999% – 11 9s), and available.

### ✅ Key Concepts

| Term       | Description                         |
|------------|-----------------------------------|
| Bucket     | Container for objects/files        |
| Object     | Data + metadata stored in a bucket |
| Key        | Unique identifier for an object    |
| Prefix     | Simulated folder structure         |
| Versioning | Keep multiple versions of an object |
| Lifecycle Rules | Transition or delete objects automatically |

### ✅ Storage Classes

| Class               | Use Case              | Durability | Availability | Cost   |
|---------------------|-----------------------|------------|--------------|--------|
| S3 Standard         | General-purpose       | 11 9s      | 99.99%       | $$     |
| S3 Intelligent-Tiering | Auto cost optimization | 11 9s      | 99.9–99.99%  | $$     |
| S3 Standard-IA      | Infrequent access     | 11 9s      | 99.9%        | $      |
| S3 One Zone-IA      | Infrequent, 1 AZ only | 11 9s      | 99.5%        | $      |
| Glacier             | Archival              | 11 9s      | Minutes–Hours| ¢      |
| Glacier Deep Archive | Long-term archive     | 11 9s      | 12–48 hours  | ¢¢     |

### ✅ S3 Features
- Event notifications (to Lambda, SNS, SQS)
- Server-side encryption (SSE-S3, SSE-KMS, SSE-C)
- Object lock (write-once-read-many)
- S3 Access Points (granular access control)
- S3 Versioning & MFA Delete
- S3 Transfer Acceleration (fast uploads)

### ✅ Security
- IAM Policies
- Bucket Policies
- ACLs (legacy)
- Block Public Access (account & bucket level)

---

## 🔷 2. Amazon EBS (Elastic Block Store)

### ✅ What is EBS?  
Block-level storage volumes for EC2 instances. Durable and low-latency.

### ✅ Volume Types

| Type   | Description          | IOPS      | Use Case         |
|--------|---------------------|-----------|------------------|
| gp3    | General purpose SSD  | Baseline 3K IOPS | Web apps, DBs     |
| io1/io2| Provisioned IOPS SSD | Up to 64K | High-performance DBs |
| st1    | Throughput HDD      | MB/s focus| Big data, logs   |
| sc1    | Cold HDD            | Low cost  | Archive          |

### ✅ Key Concepts
- Can attach/detach/move between EC2 instances (same AZ)
- Snapshots (stored in S3)
- Encryption at rest using KMS
- Multi-Attach (io1/io2 with supported EC2)

---

## 🔷 3. Amazon EFS (Elastic File System)

### ✅ What is EFS?  
Fully managed, scalable NFS file system. Can be mounted on multiple EC2/Linux instances simultaneously.

### ✅ Features
- Scales automatically
- Shared POSIX filesystem
- Bursting Throughput model
- Backup support via AWS Backup
- Available in Standard and IA modes

### ✅ Use Cases
- Shared file system
- CMS (WordPress)
- Lift-and-shift Linux applications
- HPC workloads needing POSIX compatibility

---

## 🔷 4. Amazon FSx

### ✅ What is FSx?  
Managed file systems for Windows and high-performance applications.

| Service                | Description                     | Use Case            |
|------------------------|--------------------------------|---------------------|
| FSx for Windows File Server | Native SMB, AD integration    | Windows apps        |
| FSx for Lustre         | High-performance parallel file system | HPC, ML             |
| FSx for NetApp ONTAP   | Multiprotocol (NFS, SMB), snapshots | Enterprise data     |
| FSx for OpenZFS        | POSIX, snapshots, clone         | Linux, DB apps      |

---

## 🔷 5. AWS Storage Gateway

### ✅ What is it?  
Hybrid cloud storage service that connects on-premises environments to AWS.

### ✅ Types

| Type           | Use Case                                   |
|----------------|--------------------------------------------|
| File Gateway   | NFS/SMB interface backed by S3             |
| Volume Gateway | iSCSI block access backed by EBS/S3        |
| Tape Gateway   | Replace physical tapes with virtual tapes in Glacier |

Helps in gradual cloud migration and hybrid cloud storage solutions.

---

## 🔷 6. AWS Backup

### ✅ What is it?  
Centralized backup service for:

- EBS  
- RDS  
- DynamoDB  
- EFS  
- FSx  
- Storage Gateway  
- EC2  

Supports backup vaults, lifecycle policies, and cross-region backup.

---

## 🔷 7. Data Transfer Tools

| Service               | Description                        | Use Case                    |
|-----------------------|----------------------------------|-----------------------------|
| AWS Snowball / Snowmobile | Transfer petabytes of data offline | Large-scale data migrations  |
| AWS Transfer Family   | Managed SFTP, FTPS, FTP           | Secure file transfers       |
| AWS DataSync          | Online sync between on-prem and AWS | Data migration and replication |

---
# AWS Storage Comparison Cheat Sheet – S3 vs EBS vs EFS vs FSx

| Feature            | Amazon S3                          | Amazon EBS                       | Amazon EFS                      | Amazon FSx                         |
|--------------------|----------------------------------|---------------------------------|--------------------------------|----------------------------------|
| **Type**           | Object Storage                   | Block Storage                   | File Storage                   | File Storage                     |
| **Use Case**       | Static files, backups, big data  | Boot volumes, databases, apps   | Shared file systems, container storage | Windows apps, HPC, enterprise workloads |
| **Access Method**  | HTTPS (REST API / SDK)            | Block-level (iSCSI via EC2)     | NFS (POSIX)                   | SMB, NFS, Lustre, ZFS            |
| **Latency**        | Milliseconds (non-real-time)      | Low latency, single-digit ms    | Low to moderate latency        | Low latency, optimized per FSx type |
| **Mountable**      | ❌ No                            | ✅ Yes (EC2-only, in same AZ)   | ✅ Yes (multi-AZ EC2, Linux)   | ✅ Yes (depends on FSx type)      |
| **Multi-AZ Support** | ✅ Yes (default)                | ❌ No (single AZ, except snapshots) | ✅ Yes                       | ✅ Yes (FSx ONTAP, Windows FS w/Multi-AZ) |
| **Scalability**    | Virtually unlimited               | Up to 64 TiB per volume         | Auto scales up to petabytes    | Up to multiple PB (based on FSx variant) |
| **Durability**     | 11 9s                           | 99.999%                        | 99.999999999% (with backup)   | Varies by FSx service             |
| **Throughput / IOPS** | Good for sequential             | High (provisioned IOPS for io1/io2) | Bursting model              | High performance (e.g., Lustre: 100s GB/s) |
| **OS Support**     | All platforms                    | EC2 only                      | Linux (NFS)                   | Windows/Linux (SMB, NFS, etc.)   |
| **Backup / Snapshot** | Versioning, Glacier, Backup Lifecycle | Snapshots (incremental, stored in S3) | AWS Backup                 | AWS Backup, Snapshots             |
| **Pricing Model**  | Pay per GB stored & requests     | Pay per provisioned GB & IOPS   | Pay per GB used (throughput optional) | Pay per GB + throughput/performance config |
| **Security**       | IAM, Bucket Policies, SSE        | IAM, KMS, EBS Encryption        | IAM, KMS, Mount targets, NFS ACLs | IAM, KMS, Windows ACLs, AD support |
| **Lifecycle Management** | ✅ Yes (transition to IA, Glacier) | ✅ Snapshots via backup      | ✅ Lifecycle policies with AWS Backup | ✅ Tiering & backup policies (varies by type) |
| **Common Scenarios** | Static websites, data lakes, backups | EC2 boot/data volumes, RDS, apps | Shared Linux file systems, EKS | Windows shared storage, HPC, ML, NetApp/ZFS use |

---

## 🧠 When to Use What?

| Use Case                                  | Recommended Service           |
|-------------------------------------------|------------------------------|
| Hosting static websites / data lake       | S3                           |
| Boot volumes for EC2 / databases           | EBS                          |
| Shared file system for Linux workloads     | EFS                          |
| Windows file shares / enterprise apps      | FSx for Windows              |
| High-perf ML or HPC apps needing parallel FS | FSx for Lustre             |
| POSIX-compliant shared ZFS file system     | FSx for OpenZFS              |
| Backup/restore workloads for on-prem systems | S3 + Storage Gateway       |

---

## 🧪 Pro Tips

- Use **S3 with Transfer Acceleration** for global fast uploads.
- Choose **gp3 EBS** for cost-effective block storage with customizable IOPS.
- Use **EFS IA** for cold data in shared file systems.
- Use **FSx** if your workload needs AD integration, SMB, or enterprise-grade features.

