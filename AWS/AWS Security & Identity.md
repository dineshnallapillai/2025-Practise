# 🔐 AWS Security Services Overview

---

## 1. AWS KMS (Key Management Service)

### ✅ Purpose
Securely create, store, and manage cryptographic keys for encryption/decryption in AWS.

### ✅ Key Concepts

| Term                | Description                                                      |
|---------------------|------------------------------------------------------------------|
| **CMK**             | Customer Master Key – symmetric or asymmetric encryption key     |
| **KMS Key Policies**| Define who can use/manage a key                                  |
| **Envelope Encryption**| KMS encrypts a data key, which encrypts actual data           |
| **Automatic Key Rotation**| Enabled annually for CMKs                                  |
| **HSM-backed Keys** | For compliance with FIPS 140-2 and hardware protection modules   |

### ✅ Integration
Used with: **S3, EBS, RDS, Lambda, SSM, Secrets Manager**, etc.  
Supports **client-side** (via SDK) and **server-side** (SSE-KMS) encryption.

### ✅ Interview Focus
- KMS keys are **region-specific**
- Use **symmetric CMKs** for most use cases
- Key deletion involves **7–30 day waiting period**
- Use **grants** for temporary delegated access

---

## 2. IAM Policies

### ✅ Purpose
Control **who** can do **what** on **which resources**.

### ✅ Policy Types

| Type                  | Attached To            | Use Case                              |
|-----------------------|------------------------|----------------------------------------|
| Identity-based        | Users, groups, roles   | Grants permissions to identities       |
| Resource-based        | S3, Lambda, etc.       | Define who can access a resource       |
| Permissions Boundaries| IAM roles              | Restrict maximum permissions           |
| ACLs                  | S3, VPCs, etc.         | Legacy, not IAM-based                  |

### ✅ Policy Syntax
```json
{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Allow",
    "Action": "s3:GetObject",
    "Resource": "arn:aws:s3:::mybucket/*"
  }]
}

```
## ✅ IAM Conditions

- Supports **IP**, **MFA**, **time-based**, and **tag-based** conditions.
- Wildcards allowed (e.g., `"Action": "s3:*"`).

### 🔍 Interview Focus
- IAM evaluates **all policies together**
- **Explicit Deny** overrides any Allow
- Follow the **least privilege** principle
- Combine with **permissions boundaries** and **MFA** for stronger security

---

## 🔹 3. AWS STS (Security Token Service)

### ✅ Purpose
Issue **temporary credentials** for federated users and **cross-account** access.

### ✅ Key APIs

| API                        | Use Case                                   |
|----------------------------|---------------------------------------------|
| `AssumeRole`              | Switch roles across accounts                |
| `GetSessionToken`         | Create MFA sessions                         |
| `AssumeRoleWithSAML`      | Federation with enterprise IdP (SAML)       |
| `AssumeRoleWithWebIdentity` | Auth from mobile apps (Cognito, Facebook) |

### 🔍 Interview Focus
- Default session duration is **1 hour**, extendable up to **12 hours**
- STS credentials are **temporary**, not stored in IAM
- Common in **cross-account access**, **federated auth**, and **mobile apps**

---

## 🔹 4. SCP (Service Control Policies)

### ✅ Purpose
Apply **organization-wide guardrails** to limit maximum allowed actions in **AWS Organizations**.

### ✅ Behavior
- SCPs **do not grant permissions**, they only **restrict**
- Applied to **accounts/OUs**, **not the management/root account**
- Must be combined with IAM policies for access to work

### ✅ Example SCP
```json
{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Deny",
    "Action": "*",
    "Resource": "*",
    "Condition": {
      "StringNotEquals": {
        "aws:RequestedRegion": ["us-east-1", "us-west-2"]
      }
    }
  }]
}

```
## 🔍 Interview Focus (SCPs)
- Use SCPs to **restrict AWS regions** or **deny risky services**
- SCPs are a **mandatory control layer** across **all member accounts** in AWS Organizations

---

## 🔹 5. AWS Secrets Manager

### ✅ Purpose
Securely **store, manage, and rotate secrets** like:
- Database credentials  
- API keys  
- OAuth tokens  

### ✅ Features
- ✅ **Auto-rotation** of secrets (integrated with AWS Lambda)
- ✅ **KMS-based encryption**
- ✅ **Fine-grained IAM controls**
- ✅ **Auditing** via AWS CloudTrail

---

### 🔁 Secrets Manager vs SSM Parameter Store

| Feature                  | Secrets Manager      | SSM Parameter Store (Advanced) |
|--------------------------|----------------------|--------------------------------|
| **Auto-rotation**        | ✅ Yes               | ❌ Manual only                 |
| **Native RDS Integration** | ✅ Yes             | ❌ No                          |
| **Cost**                 | 💲 Paid per use      | ✅ Basic tier is free          |
| **Encryption**           | ✅ via KMS           | ✅ via KMS                     |
| **API Integration**      | ✅ SDK, CLI, Lambda  | ✅ SDK, CLI                    |

---

## 🔒 Security Best Practices

- ✅ Prefer **IAM Roles** over long-lived IAM users
- ✅ Enforce **MFA** for all **high-privilege** IAM users
- ✅ Use **resource-based policies** for secure **cross-account access**
- ✅ **Rotate secrets** regularly using AWS Secrets Manager
- ✅ Apply **SCPs** to restrict disallowed **services** and **regions**
- ✅ **Encrypt everything** using **AWS KMS**
- ✅ Enable **CloudTrail** to **audit access** to IAM, KMS, and Secrets

---
## 🔐 IAM Policies vs SCPs

| Feature               | IAM Policies                                      | Service Control Policies (SCPs)                          |
|-----------------------|--------------------------------------------------|-----------------------------------------------------------|
| Purpose               | Grant or deny permissions to IAM identities      | Set maximum permissions for AWS accounts (guardrails)    |
| Applied To            | Users, groups, roles                             | AWS Organizations → OUs or accounts                      |
| Grants Permissions?   | ✅ Yes                                            | ❌ No (only restricts)                                   |
| Scope                 | Resource-level, fine-grained                     | Account-level (broad)                                    |
| Evaluation Behavior   | Allow if IAM allows and SCP allows               | Deny if SCP denies, even if IAM allows                   |
| Use Case              | Day-to-day access control                        | Org-wide compliance and governance                       |

---

## 📤 Sharing S3 Access Across AWS Accounts

✅ **Options:**
1. **Bucket Policy** – Add a resource-based policy allowing another account's IAM principal.
2. **ACLs (Legacy)** – Not recommended, less secure and flexible.
3. **Cross-account IAM Role** – One account assumes a role in the S3-owning account.

✅ **Best Practice:**  
Use **resource-based policy** on the bucket with proper `Principal` and permissions.

---

## ❌ IAM Allows but SCP Denies?

- **Result:** The action is **denied**.
- **SCPs set the outer boundary** – if an action is denied by SCP, IAM permissions don’t matter.

---

## 🔑 KMS Integration with S3 & Lambda

### ✅ Amazon S3:
- Use **SSE-KMS** for server-side encryption with a KMS key.
- S3 encrypts/decrypts data using the **KMS Customer Managed Key (CMK)**.
- Control access via **IAM + KMS key policy**.

### ✅ AWS Lambda:
- Lambda can **decrypt environment variables** using KMS.
- Use **KMS to encrypt secrets** in code/configs.
- Grant the Lambda execution role **`kms:Decrypt`** permission.

---

## 🔄 When to Use STS Instead of IAM Role?

| Use Case                          | Use STS                                |
|----------------------------------|----------------------------------------|
| Cross-account access             | ✅ AssumeRole across accounts          |
| Temporary credentials            | ✅ Limited-time access (up to 12 hours)|
| Federation (SSO, SAML, web apps) | ✅ AssumeRoleWithSAML or WebIdentity   |
| MFA sessions                     | ✅ GetSessionToken                     |

**STS issues temporary credentials** – ideal for delegation, federation, or just-in-time access.

---

## 🔐 Secrets Manager vs SSM Parameter Store

| Feature                   | Secrets Manager          | SSM Parameter Store (Advanced Tier) |
|---------------------------|--------------------------|-------------------------------------|
| Auto-Rotation             | ✅ Yes (with Lambda)     | ❌ Manual                           |
| Native RDS Integration    | ✅ Yes                  | ❌ No                               |
| Cost                      | 💲 Paid per secret       | ✅ Free tier available              |
| Encryption via KMS        | ✅ Yes                  | ✅ Yes                              |
| Audit via CloudTrail      | ✅ Yes                  | ✅ Yes                              |
| JSON Secret Storage       | ✅ Yes                  | ✅ Yes                              |

**Recommendation:**  
Use **Secrets Manager** for credentials needing rotation.  
Use **SSM Parameter Store** for general config values or secrets with lower rotation needs.

---

## 🔒 Enforcing MFA for IAM Users

### ✅ Method:
Use **IAM policy condition** with `aws:MultiFactorAuthPresent`.

```json
{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Deny",
    "Action": "*",
    "Resource": "*",
    "Condition": {
      "BoolIfExists": {
        "aws:MultiFactorAuthPresent": "false"
      }
    }
  }]
}

```
# AWS Security Specialty – Deep Dive  
*(GuardDuty, Macie, Detective, Inspector, IAM Access Analyzer)*

---

## 🔐 Overview

| Service         | Purpose                                   | Type          |
|-----------------|-------------------------------------------|---------------|
| **GuardDuty**     | Threat detection                         | Detection     |
| **Macie**         | Sensitive data discovery (e.g., PII)     | Data Protection |
| **Detective**     | Investigate incidents & trace root cause | Investigation |
| **Inspector**    | Automated vulnerability assessment        | Assessment    |
| **Access Analyzer** | Analyze IAM & resource access paths       | Access Visibility |

---

## 1. Amazon GuardDuty

### ✅ Purpose  
Continuous monitoring to detect malicious behavior and unauthorized activity in AWS accounts, workloads, and data.

### ✅ Key Features

| Feature          | Description                                    |
|------------------|------------------------------------------------|
| Threat detection | Analyzes logs from VPC Flow, DNS, CloudTrail  |
| No agents required | Fully managed & agentless                      |
| Findings        | Includes severity, resource, recommendation    |
| S3 Protection    | Alerts on suspicious access to S3 data          |
| Malware Protection | Analyzes EBS volumes (optional)                |

### ✅ Example Detections

- SSH brute force to EC2  
- Credential exfiltration (API key abuse)  
- Crypto mining behavior  
- Suspicious S3 API calls  

### ✅ Integrations

- EventBridge – Auto remediation via Lambda  
- Security Hub – Centralized visibility  

### ✅ Interview Tips

- Real-time & agentless  
- Covers EC2, EKS, Lambda, S3, IAM, etc.  
- Use to reduce detection time during breaches  

---

## 2. Amazon Macie

### ✅ Purpose  
Discover and protect sensitive data (PII, PHI, secrets) in S3 buckets.

### ✅ Key Features

| Feature          | Description                               |
|------------------|-------------------------------------------|
| PII detection    | Detect names, emails, credit cards, etc.  |
| Fully managed   | No need to write regex or patterns          |
| Data classification | Uses ML to label objects (low, medium, high risk) |
| Alerts           | When sensitive data is exposed or publicly accessible |

### ✅ Common Use Cases

- Identify unencrypted PII data in S3  
- Detect overly permissive access  
- Compliance audits (HIPAA, GDPR)  

### ✅ Interview Tips

- Only supports S3  
- Macie uses KMS for encryption  
- Outputs go to CloudWatch, EventBridge, Security Hub  

---

## 3. Amazon Detective

### ✅ Purpose  
Helps investigate security findings across accounts using graphs & relationship links.

### ✅ Data Sources

- VPC Flow Logs  
- CloudTrail  
- GuardDuty findings  
- IAM events  

### ✅ Features

| Feature           | Description                                       |
|-------------------|--------------------------------------------------|
| Behavior graphs   | Visualize activity over time per entity            |
| Entity-based views | Investigate EC2, users, IPs, roles                 |
| Linked events     | Automatically correlates findings (user → EC2 → API call) |
| No manual log parsing | Removes need to deep-dive raw logs              |

### ✅ Interview Tips

- Detective does not prevent threats – it helps analyze  
- Can be used post-breach to understand lateral movement  
- Integrates with GuardDuty and Security Hub  

---

## 4. Amazon Inspector

### ✅ Purpose  
Perform automated vulnerability scanning for EC2, ECR, Lambda functions.

### ✅ Key Capabilities

| Capability        | Description                                    |
|-------------------|------------------------------------------------|
| EC2 scanning     | Looks for CVEs, misconfigurations               |
| ECR scanning     | Scans container images on push                   |
| Lambda scanning  | Analyzes function packages for issues           |
| OS & App CVEs    | CVE-based detection (CVSS scoring)               |
| Agentless (v2)   | No longer requires SSM agent in Inspector v2    |

### ✅ Common Findings

- Unpatched packages  
- Open ports  
- Privilege escalation risks  

### ✅ Interview Tips

- Replaces manual scanning tools  
- Fully integrates with AWS Organizations  
- Inspector v2 supports continuous scanning  
- Uses EventBridge for remediation flows  

---

## 5. IAM Access Analyzer

### ✅ Purpose  
Helps identify unintended resource access and validate least privilege policies.

### ✅ Use Cases

| Use Case              | Description                                  |
|-----------------------|----------------------------------------------|
| Policy validation      | Detects unused permissions                    |
| Cross-account access analysis | Who can access a resource externally   |
| Access preview        | What happens if a policy is applied           |
| Auto-analyze on save  | Optional feature to validate new IAM/S3/KMS/Lambda policies |

### ✅ Supported Resources

- S3  
- KMS  
- Lambda  
- IAM Roles  
- SQS, Secrets Manager  

### ✅ Interview Tips

- Does not prevent access — only analyzes it  
- Can automatically scan policies when saved (optional)  
- Helps enforce least privilege  

---

## 📊 Comparison Table

| Feature               | GuardDuty          | Macie             | Detective         | Inspector        | Access Analyzer  |
|-----------------------|--------------------|-------------------|-------------------|------------------|------------------|
| Detection Type        | Threats & attacks  | Sensitive data    | Root-cause tracing | Vulnerabilities  | Access risks     |
| Works on              | Logs (CloudTrail, DNS) | S3 only           | All security logs | EC2, ECR, Lambda | IAM + Resource   |
| Agentless?            | ✅                 | ✅                | ✅                | ✅ (v2)          | ✅               |
| Cost Model            | Per GB logs        | Per GB scanned    | Based on logs     | Per scan/resource| Free             |
| Preventive or Reactive? | Reactive          | Preventive        | Reactive          | Preventive       | Preventive       |
| Integrates with Security Hub? | ✅          | ✅                | ✅                | ✅               | ✅               |

---

## 🔐 Security Best Practices

- ✅ Enable GuardDuty on all accounts via AWS Organizations  
- ✅ Use Macie for regulated S3 datasets (HIPAA, GDPR)  
- ✅ Schedule Inspector scans for EC2/ECR on deployment  
- ✅ Enforce least privilege with Access Analyzer  
- ✅ Route all findings to AWS Security Hub  
- ✅ Set up automated remediation using EventBridge + Lambda  

---

# AWS Security Specialty – Key Questions

---

## 1. What's the difference between GuardDuty and Inspector?

| Aspect              | GuardDuty                              | Inspector                                  |
|---------------------|--------------------------------------|--------------------------------------------|
| Purpose             | Threat detection & continuous monitoring for suspicious activity | Automated vulnerability assessment & scanning of EC2, ECR, Lambda |
| Data Sources        | Analyzes logs: VPC Flow, DNS, CloudTrail | Scans OS, application vulnerabilities, CVEs, misconfigurations |
| Agent Requirement   | Agentless, fully managed              | Agentless (Inspector v2), but targets specific resources |
| Reactive or Preventive | Reactive (detects attacks/behavior) | Preventive (identifies vulnerabilities before exploitation) |
| Integration         | EventBridge, Security Hub             | EventBridge, Security Hub                   |

---

## 2. How do you detect exposed PII in AWS?

- Use **Amazon Macie** to scan S3 buckets for sensitive data (PII, PHI, secrets).  
- Macie uses ML to automatically classify and detect exposed or publicly accessible PII.  
- Set up alerts and reports for compliance and remediation.  
- Integrate Macie findings with CloudWatch, EventBridge, or Security Hub for automated response.

---

## 3. How would you investigate a suspicious API call?

- Start with **Amazon GuardDuty** findings to detect suspicious activity.  
- Use **Amazon Detective** to investigate by visualizing relationships, behavior graphs, and linked events.  
- Correlate API call logs with CloudTrail events, VPC Flow Logs, and IAM logs for context.  
- Identify entities involved (users, roles, IP addresses) and trace lateral movement or impact.  
- Use Detective’s entity-based views to follow the activity timeline without manual log parsing.

---

## 4. How to ensure IAM roles/policies don’t over-provision access?

- Use **IAM Access Analyzer** to validate IAM roles and policies for least privilege.  
- Review access previews to understand what permissions policies grant.  
- Detect unused or excessive permissions with Access Analyzer reports.  
- Implement a policy review process and automate scans on policy changes.  
- Enforce MFA and use resource-based policies to tighten access scope.  

---

## 5. When would you use Detective vs CloudTrail analysis manually?

| Criteria                      | Amazon Detective                            | Manual CloudTrail Analysis                     |
|-------------------------------|--------------------------------------------|-----------------------------------------------|
| Use Case                     | Complex incident investigation & root cause analysis | Ad-hoc or simple queries on logs                |
| Ease of Use                  | Visual graphs, entity linking, automatic correlation | Raw logs parsing, manual correlation             |
| Time Efficiency             | Faster to find lateral movement & relationships | Time-consuming, error-prone                      |
| Integration                 | Works with GuardDuty, Security Hub for streamlined workflow | Requires manual setup or additional tooling     |
| Skill Level Required        | Lower for complex investigations           | Higher expertise for deep log queries            |

---

