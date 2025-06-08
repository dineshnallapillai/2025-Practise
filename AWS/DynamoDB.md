# 🗃️ Amazon DynamoDB Overview

Amazon DynamoDB is a fully managed NoSQL database service designed for single-digit millisecond performance at any scale.

---

## 🚀 Core Features

| Feature                 | Description                                      |
|------------------------|--------------------------------------------------|
| Fully managed          | No infrastructure, scaling, patching required   |
| Serverless             | Auto scales to handle any workload              |
| NoSQL                  | Key-Value + Document-based                      |
| Multi-Region, Multi-AZ | High availability and durability                |
| Millisecond latency    | Low latency even at massive scale               |
| Fine-grained access    | IAM-based security                              |
| Streams                | Change data capture (CDC) with DynamoDB Streams |
| On-demand or provisioned | Flexible capacity models                      |

---

## 🧱 Table Basics

| Concept        | Description                             |
|----------------|-----------------------------------------|
| Table          | Collection of items                     |
| Item           | A row (JSON-like document)              |
| Attribute      | A column (key-value pair)               |
| Partition Key  | Required primary key                    |
| Sort Key       | Optional, enables range queries         |

---

## 🔑 Primary Key Types

| Type      | Structure                   | Use Case                                |
|-----------|-----------------------------|------------------------------------------|
| Simple    | Partition Key only          | Fast lookup by unique ID                |
| Composite | Partition Key + Sort Key    | Query related items (e.g., orders by user) |

**Example:**

```json
Table: Orders
Partition Key: CustomerId
Sort Key: OrderDate

{
  "CustomerId": "123",
  "OrderDate": "2024-10-01",
  "Amount": 199.99
}

```
# 🗃️ DynamoDB Indexes

## 1️⃣ Local Secondary Index (LSI)
- Same Partition Key, different Sort Key
- Created at table creation
- Max 5 LSIs per table
- Supports strong consistency

## 2️⃣ Global Secondary Index (GSI)
- Different Partition Key and Sort Key
- Can be created any time
- Supports eventual consistency only
- Max 20 GSIs per table

| Feature           | LSI                    | GSI                      |
|-------------------|-------------------------|---------------------------|
| Partition Key     | Same as base table      | Different from base table |
| Sort Key          | Different               | Different                 |
| Created When      | On table creation       | Any time                  |
| Read Consistency  | Strong / Eventual       | Eventual only             |

---

# 💾 Capacity Modes

## 🟢 On-Demand (Recommended)
- Auto-scales instantly
- Pay per request (WCU/RCU)
- No provisioning required

## ⚙️ Provisioned
- Fixed read/write capacity
- Can use Auto Scaling
- Best for predictable workloads

---

# 🔁 Streams & Triggers

| Feature             | Description                              |
|---------------------|------------------------------------------|
| DynamoDB Streams    | Captures inserts/updates/deletes         |
| Use Cases           | Audit, CDC, Replication, Triggers        |
| Integrates with     | AWS Lambda, Kinesis                      |

```text
[ DynamoDB Table ] → [ DynamoDB Stream ] → [ Lambda Function ]
```

---
# 🚥 Access Patterns

| Access Pattern                 | Solution                                       |
|-------------------------------|------------------------------------------------|
| Lookup by ID                  | Use Partition Key                              |
| Time-series data              | Use Composite key (e.g., user + timestamp)     |
| Query latest N items per user | Use Sort Key + `ScanIndexForward = false`     |
| Filter by non-key attribute   | Use GSI or query + filter                      |
| Multiple entity types         | Use single-table design                        |

---

# 🧰 Single-Table Design

- Use a single DynamoDB table to store multiple entity types
- Leverage `PK`, `SK`, and `EntityType` attributes

```json
{
  "PK": "USER#123",
  "SK": "PROFILE",
  "Name": "Alice"
}

{
  "PK": "USER#123",
  "SK": "ORDER#2024-10-01",
  "Amount": 100
}

```
---
## ⏱️ Time To Live (TTL)

- Items are **auto-deleted** after a specified expiration timestamp.
- Ideal for **session management** or **temporary data storage**.

---

## 🔒 Security

- **IAM-based access control** for fine-grained permissions.
- **Encryption at rest** using **AWS KMS**.
- **VPC endpoints** for secure, private access.

---

## 📉 Backup & Restore

| Feature                | Description                                      |
|------------------------|--------------------------------------------------|
| On-Demand Backup       | Manual snapshots of tables                       |
| Point-in-Time Recovery | Restore to any second in the last 35 days        |
| Export to S3           | Data export in **Parquet** format                |

---

## 🔄 DynamoDB vs RDBMS

| Feature       | DynamoDB           | Relational DB         |
|---------------|--------------------|------------------------|
| Schema        | Schema-less         | Fixed schema           |
| Joins         | ❌ Not supported     | ✅ Yes                 |
| Transactions  | ✅ Limited           | ✅ Full ACID           |
| Scalability   | ✅ Horizontal        | Mostly vertical        |
| Latency       | Millisecond-level   | Varies                 |
| Cost          | Pay per usage       | Pay per instance       |

---
## 📌 DynamoDB Interview Q&A

### 🔑 What are Partition and Sort Keys?

- **Partition Key**: A mandatory primary key that determines the partition (physical storage) for an item. It must be **unique** for simple key tables.
- **Sort Key**: Optional. When combined with the partition key, it allows storing **multiple related items** under the same partition and enables **range queries**.

---

### 🔄 Difference Between GSI and LSI

| Feature             | LSI                               | GSI                              |
|---------------------|------------------------------------|----------------------------------|
| Partition Key       | Same as base table                 | Different from base table        |
| Sort Key            | Different from base table          | Different from base table        |
| Creation Time       | Only at table creation             | Can be created anytime           |
| Read Consistency    | Supports **strong** and eventual   | **Eventually consistent** only   |
| Max per Table       | 5                                  | 20                               |

---

### 🌍 How Does DynamoDB Achieve High Availability?

- **Multi-AZ replication** across Availability Zones for fault tolerance.
- **Automatic partitioning** and horizontal scaling.
- **Data stored on SSDs** with automatic replication and backup.
- Optional **Global Tables** for multi-region availability.

---

### 🔗 How to Model Many-to-Many Relationships?

Use **composite keys** and **inverted indexes**. For example:

```json
{
  "PK": "STUDENT#123",
  "SK": "COURSE#ENG101"
}
{
  "PK": "COURSE#ENG101",
  "SK": "STUDENT#123"
}

```
This allows querying all courses for a student and all students in a course.

---
## 🔥 How to Handle Hot Partitions?

- Use **high-cardinality** partition keys to evenly distribute writes.
- Add **random suffixes or prefixes** to keys to avoid concentrated access.
- Use **on-demand capacity** to automatically scale based on workload.
- Monitor performance using **CloudWatch metrics** like `ConsumedWriteCapacityUnits`.

---

## 📄 How to Implement Pagination?

- Use the `LastEvaluatedKey` returned by a `Query` or `Scan` operation.
- Pass it as `ExclusiveStartKey` in the next request to retrieve the next set of items.
- Repeat the process until `LastEvaluatedKey` is `null`.

---

## ⚙️ What are WCU and RCU? How Are They Calculated?

- **WCU (Write Capacity Unit)**:  
  - 1 WCU = 1 write of up to 1 KB per second.

- **RCU (Read Capacity Unit)**:  
  - 1 RCU = 1 strongly consistent read of up to 4 KB per second.  
  - Or, 2 eventually consistent reads of up to 4 KB per second.

- **Calculation Formula**:  

Total WCU or RCU = (Item size / Unit size) × Number of operations


---

## 🧱 When to Use Single-Table Design?

- When aiming to **reduce the number of tables** and centralize access.
- Ideal for **multi-entity systems** (e.g., users, orders, products).
- Provides **high scalability** and allows for **optimized query access** patterns.
- Use patterns like:
```text
PK = USER#123
SK = ORDER#2024-01-01
```
---
## 🔁 How Does DynamoDB Streams + Lambda Work?

- **DynamoDB Streams** capture real-time data changes such as:
  - `INSERT`
  - `MODIFY`
  - `REMOVE`

- These stream records are automatically sent to an **AWS Lambda** function.

- The **Lambda function** processes the changes, enabling:
  - ✅ **Data replication** to other systems or tables
  - ✅ **Triggering downstream workflows**
  - ✅ **Writing audit logs** or metrics

### 🛠️ Architecture

[DynamoDB Table] → [DynamoDB Stream] → [Lambda Function]

This setup enables **event-driven processing** with low latency and high scalability.









