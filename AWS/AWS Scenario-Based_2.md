## 91. What’s your approach for migrating a large monolithic on-prem app to AWS?  
✅ **Solution:**  
### Discovery:
- Use AWS Application Discovery Service, Migration Hub  

### Assess:
- Classify workloads (Lift & Shift, Refactor, Replace)  

### Plan:
- Apply 6 R’s:  
  - Rehost  
  - Replatform  
  - Repurchase  
  - Refactor  
  - Retire  
  - Retain  

### Migrate:
- Rehost → AWS Application Migration Service (MGN)  
- DB → AWS DMS  
- Files → AWS DataSync  

### Post-Migration:
- Validate via CloudEndure cutover tests  
- Optimize costs using Compute Optimizer  

---

## 92. How do you migrate a SQL Server database with minimal downtime?  
✅ **Solution:**  
- Use AWS DMS with Full Load + CDC (Change Data Capture)  
- Source: On-prem SQL Server  
- Target: RDS for SQL Server or EC2 SQL Server  
- Validate with Schema Conversion Tool  
- Cutover when replication lag = 0  
- Use RDS Multi-AZ for high availability  

---

## 93. Set up a hybrid architecture where sensitive data must remain on-prem  
✅ **Solution:**  
- Use AWS Outposts or Snowball Edge  
- Direct Connect or Site-to-Site VPN  
- Store sensitive data on-prem, send metadata to AWS  
- Use Amazon S3 Access Points with VPC restrictions  
- IAM + AD Connector for access control  

---

## 94. How do you support disconnected edge locations in AWS?  
✅ **Solution:**  
- Use AWS Snowball Edge (with compute) or AWS IoT Greengrass  
- Local processing, sync to S3 periodically  
- Use Lambda@Edge for near-user compute  
- Edge app updates via Systems Manager  
- Use cases: ships, oil rigs, military, retail POS  

---

## 95. Your customer wants to move a legacy Oracle ERP to AWS. What do you do?  
✅ **Solution:**  
- Rehost on EC2 with Oracle BYOL  
- Use DMS for schema and data migration  
- Use EBS-optimized EC2 with high IOPS  
- Optionally containerize via ECS  
- Use Direct Connect for network access  
- Monitor via CloudWatch + Trusted Advisor  

---

## 96. How to enforce governance across 20 AWS accounts?  
✅ **Solution:**  
- Use AWS Organizations  
- OU structure: Prod, Dev, Sandbox  
- Apply Service Control Policies (SCPs):  
  - Deny unapproved regions  
  - Deny specific instance types  
  - Deny root access  
- Bootstrap accounts via Control Tower  
- Centralized logging via Config Aggregator, CloudTrail, Security Hub  
- Automate remediation with Config Rules + Lambda  

---

## 97. Design a scalable content delivery architecture with global users  
✅ **Solution:**  
- Host content in Amazon S3  
- Distribute via CloudFront:  
  - Edge caching  
  - Geo restrictions  
- Use Lambda@Edge for personalization and auth  
- Secure origin with Origin Access Control (OAC)  
- Enable CloudFront logs → S3 + Athena  

---

## 98. Provide secure access to AWS resources for on-prem developers  
✅ **Solution:**  
- Use AWS SSO + Directory Service (AD Connector)  
- Federate AD with IAM roles  
- Optionally use SAML 2.0  
- Apply IAM fine-grained permissions  
- Monitor via CloudTrail and CloudWatch  

---

## 99. What are the options for migrating petabyte-scale data to AWS?  
✅ **Solution:**  

| Use Case                      | Recommended Service         |
|------------------------------|-----------------------------|
| TB–PB of static files         | AWS Snowball Edge           |
| Streaming or ongoing sync     | AWS DataSync                |
| Database migration            | AWS DMS                     |
| Real-time apps                | Direct Connect              |
| Massive archive bulk upload   | AWS Snowmobile (up to 100PB)|

---

## 100. Ensure regulatory compliance (e.g. HIPAA, GDPR) on AWS workloads  
✅ **Solution:**  
- Use AWS Artifact for compliance reports  
- Enable encryption (KMS, S3 Object Lock)  
- Apply IAM least privilege + SCPs  
- Use Config Rules, CloudTrail, and Security Hub  
- Document architecture via Well-Architected Tool  
- Restrict data residency via region control  

---

## 101. How to build a hybrid backup and restore system?  
✅ **Solution:**  
- On-prem → AWS Backup or S3 via DataSync  
- Use Storage Gateway (File or Tape Gateway)  
- Store in S3 Glacier / Glacier Deep Archive  
- Restore from AWS to on-prem via Gateway  

---

## 102. Compute-intensive workloads in remote, offline locations  
✅ **Solution:**  
- Use Snowball Edge Compute Optimized  
- Run EC2 AMIs on device  
- Local storage and processing  
- Sync to S3 when reconnected  
- Use Greengrass + Lambda for edge logic  

---

## 103. Provide cost governance in a multi-account AWS environment  
✅ **Solution:**  
- Use AWS Organizations + Consolidated Billing  
- Set Budgets & Budget Actions per account/OU  
- Monitor via Cost Explorer & Anomaly Detection  
- Tag resources (Owner, CostCenter, Env)  
- Generate detailed reports using AWS CUR  

---

## 104. Secure cross-account communication in a hybrid cloud setup  
✅ **Solution:**  
- Use VPC Endpoint Services + PrivateLink  
- Apply resource-based policies (S3, Lambda, etc.)  
- Authenticate using IAM roles with external ID  
- Encrypt traffic with TLS  
- Use VPC Peering / Transit Gateway  
- Monitor via VPC Flow Logs and GuardDuty  

---

## 105. Modernize legacy mainframe workflows on AWS  
✅ **Solution:**  
- Analyze code via Mainframe Modernization Hub  
- Convert COBOL → Java (Blu Age or Micro Focus)  
- Migrate data to Aurora PostgreSQL / RDS Oracle  
- Containerize with ECS Fargate  
- Orchestrate jobs using Step Functions
  
---

## 106. Design a multi-region, highly available web application  
✅ **Solution:**  
**Frontend:**  
- Route 53 with latency-based routing  
- Static content via CloudFront  

**App Layer:**  
- Deploy to two regions using Elastic Beanstalk or ECS  

**DB Layer:**  
- Aurora Global Database for low-latency reads  
- DynamoDB Global Tables for NoSQL  

**Failover:**  
- Route 53 health checks for traffic rerouting  

**Data Sync:**  
- S3 Cross-Region Replication  
- AWS DataSync for file transfers  

---

## 107. Deploy an AI model to edge devices using AWS  
✅ **Solution:**  
- Use AWS IoT Greengrass  
- Deploy models (TensorFlow Lite, PyTorch) to edge  
- Handle offline inferencing  
- Train in SageMaker  
- Package model as ML component for Greengrass  
- Monitor via IoT Device Defender  
- Sync results to S3 or Kinesis Data Firehose  

---

## 108. How do you implement Zero Trust Architecture on AWS?  
✅ **Solution:**  
**Principles:**  
- Never trust, always verify  
- Least privilege, contextual enforcement  

**AWS Components:**  
- IAM Identity Center (SSO) + MFA  
- Resource-based IAM policies  
- VPC, Subnets, NACLs for network segmentation  
- PrivateLink & VPC Endpoints  
- AWS Shield, WAF, CloudFront  
- CloudTrail, GuardDuty, Security Hub  

---

## 109. Build a greenfield SaaS platform for global customers  
✅ **Solution:**  
**Multi-tenancy:**  
- Partitioned DynamoDB or separate AWS accounts  

**Auth:**  
- Amazon Cognito + IAM Roles  
- SAML/OIDC integration  

**Architecture:**  
- Serverless: API Gateway + Lambda + DynamoDB  
- Containers: ECS Fargate / EKS  

**Observability:**  
- CloudWatch Logs, X-Ray  

**Cost:**  
- Tag resources by tenant  
- Use Cost & Usage Reports (CUR)  

---

## 110. How do you design for region-wide AWS failure?  
✅ **Solution:**  
- Replicate to a secondary region:  
  - S3: Cross-Region Replication  
  - DynamoDB: Global Tables  
  - Aurora: Global Database  
- Route 53 with failover routing  
- Infrastructure as code (CDK, CloudFormation)  
- Test with AWS Fault Injection Simulator  

---

## 111. How do you push software updates to millions of devices securely?  
✅ **Solution:**  
- AWS IoT Device Management  
- Use fleet indexing + device groups  
- OTA jobs for updates  
- Sign updates with KMS  
- Use IAM roles for device authentication  
- Monitor with IoT Events + CloudWatch  

---

## 112. Secure communication between microservices across accounts  
✅ **Solution:**  
- PrivateLink + VPC Endpoint Services  
- Cross-account IAM roles with external ID  
- Encrypt traffic with TLS + ACM certificates  
- Monitor using VPC Flow Logs + GuardDuty  

---

## 113. Build a disaster-resilient big data pipeline (Petabyte scale)  
✅ **Solution:**  
**Ingestion:**  
- Kinesis Data Streams / Firehose  

**Storage:**  
- S3 with versioning + replication  

**Processing:**  
- EMR, Glue, or Athena  

**Disaster Recovery:**  
- Enable S3 CRR  
- Keep warm standby in secondary region  

---

## 114. Greenfield app: how do you choose between Lambda and ECS?  
✅ **Decision Matrix:**

| Criteria                    | AWS Lambda               | ECS Fargate             |
|----------------------------|--------------------------|--------------------------|
| Startup Latency            | Cold starts (ms–sec)     | No cold starts          |
| Runtime Limit              | Max 15 mins              | No limit                |
| Long-running / background  | ❌                        | ✅                      |
| Predictable load           | ✅                        | ✅                      |
| Heavy compute / GPU        | ❌                        | ✅ (EC2-backed)         |
| CI/CD Tooling              | SAM, CDK                 | ECS + CodePipeline      |

---

## 115. Build a cross-region container-based SaaS platform with latency optimization  
✅ **Solution:**  
- Use ECS Anywhere / EKS + AWS Global Accelerator  
- Route 53 with latency routing  
- Multi-region ECS clusters  
- Aurora Global DB + DynamoDB Global Tables  
- Kinesis Data Firehose → cross-region log aggregation to S3  

---

## 116. How to enforce security baselines and compliance in greenfield projects  
✅ **Solution:**  
- AWS Control Tower for guardrails  
- Service Control Policies (SCPs)  
- AWS Config + Config Rules  
- Centralized CloudTrail + Security Hub  
- Audit Manager for compliance reporting  

---

## 117. Edge AI with drone video feeds — what architecture fits?  
✅ **Solution:**  
**Onboard Inference:**  
- Lightweight ML with Greengrass  
- Local video storage (disk or EBS edge)  

**Heavy Compute (Offline):**  
- Use Snowball Edge Compute Optimized  

**Data Handling:**  
- Sync metadata/results to S3  
- Archive video to S3 Glacier  

---

## 118. How would you architect zero downtime blue/green Lambda deployments?  
✅ **Solution:**  
- Use Lambda Aliases + Versions  
- Weighted Routing via Lambda Alias Traffic Shifting  
- CodeDeploy for traffic shifting + rollback  
- Validate with pre/post hooks  

---

## 119. Cost optimization for a high-throughput serverless workload  
✅ **Solution:**  
- Use Lambda Power Tuning  
- Reduce invocations with SQS batching  
- Use Provisioned Concurrency only where needed  
- Archive old data to S3 Glacier Deep Archive  
- Use Athena instead of Redshift for ad hoc queries  

---

## 120. Architect an IoT + AI solution for predictive maintenance in remote locations  
✅ **Solution:**  
- Devices send telemetry via AWS IoT Core  
- Store in Timestream / DynamoDB  
- Train ML model in SageMaker  
- Deploy to edge with Greengrass  
- Detect anomalies with CloudWatch Alarms + IoT Events  
 
