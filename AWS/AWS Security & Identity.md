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

