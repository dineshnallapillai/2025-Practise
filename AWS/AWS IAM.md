# AWS IAM (Identity and Access Management)

IAM is the core security layer in AWS. Mastering IAM is essential because every action in AWS is gated by IAM permissions.

We’ll cover everything you need to know—concepts, policies, advanced features, and real-world design decisions.

---

## ✅ 1. What is IAM?

IAM is a global service that allows you to securely control access to AWS services and resources for your users.

- You create users, groups, and roles  
- You assign permissions via policies  
- IAM enables least privilege, segmentation, and auditability  

---

## ✅ 2. Core IAM Components

| Component         | Description                                                   |
|------------------|---------------------------------------------------------------|
| **User**         | Represents a person or application needing access             |
| **Group**        | Collection of IAM users (e.g., Admins, Developers)            |
| **Role**         | Set of permissions assumed by trusted identities              |
| **Policy**       | Document (JSON) that defines what actions are allowed/denied  |
| **Identity Provider (IdP)** | Federation with external systems like SAML, OIDC   |
| **Session Token**| Temporary credentials issued via STS                          |

---

## ✅ 3. IAM Policies

### ➤ What is a policy?

A JSON document that defines:

- **Who** can perform  
- **What** action  
- **On what** resource  
- **Under what** condition  

### ➤ Types of Policies

| Type                    | Use Case                                                  |
|-------------------------|-----------------------------------------------------------|
| **Identity-Based Policy**   | Attached to users, groups, or roles                   |
| **Resource-Based Policy**   | Attached directly to resources (e.g., S3 bucket policy)|
| **Permissions Boundary**    | Restricts max permissions a user/role can get         |
| **Service Control Policy (SCP)** | Org-level restriction across accounts (via AWS Organizations) |
| **Session Policies**        | Applied when assuming a role (temporary credentials)  |

### ➤ Sample Policy:

```json
{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Allow",
    "Action": ["s3:GetObject"],
    "Resource": ["arn:aws:s3:::my-bucket/*"]
  }]
}

```
---
## ✅ 4. Roles & Temporary Access (STS)

IAM Roles are assumed by:

- AWS services (e.g., EC2 assumes role to access S3)
- Users from another AWS account
- Federated users (via SAML, OIDC, Cognito)
- Lambda functions

🔁 **Temporary credentials** are issued via **STS (Security Token Service)**.

---

## ✅ 5. IAM Best Practices (from whitepaper)

| Practice                        | Explanation                                              |
|--------------------------------|----------------------------------------------------------|
| 🔐 **Least Privilege**          | Always grant minimum permissions                         |
| ✅ **MFA Everywhere**           | Enforce MFA for console users                            |
| 📁 **Use Roles, not access keys** | Especially for EC2, Lambda, ECS                       |
| 🔄 **Rotate credentials**       | Use Secrets Manager for automation                       |
| 👨‍👩‍👧‍👦 **Groups for permissions** | Attach policies to groups, not individuals            |
| 📜 **Use Managed Policies wisely** | Start with AWS-managed; move to custom for control   |
| 📊 **Audit with CloudTrail & Access Analyzer** | Check what permissions are actually used          |

---

## ✅ 6. IAM vs Resource Policy Example

| Task                                         | Use IAM Policy | Use Resource Policy         |
|---------------------------------------------|----------------|------------------------------|
| Grant user in your account access to S3     | ✅             | ❌                           |
| Grant another account access to your S3     | ❌             | ✅ (bucket policy)           |

---

## ✅ 7. Key Advanced IAM Features

| Feature               | Description                                                |
|-----------------------|------------------------------------------------------------|
| **IAM Access Analyzer** | Detects risky permissions, cross-account access          |
| **Policy Simulator**     | Test what a policy will allow or deny                  |
| **Service-Linked Roles** | AWS creates/manages them (e.g., for Lambda, ECS)       |
| **Conditions**           | Add logic like `IpAddress`, `MultiFactorAuthPresent`   |

---

## ✅ 8. Federation (SAML / OIDC / Cognito)

| Federation Type | Use Case                                      |
|------------------|----------------------------------------------|
| **SAML 2.0**      | Corporate AD → SSO into AWS                 |
| **OIDC**          | Login via Google, Facebook, etc.            |
| **Cognito**       | Auth for mobile/web apps with social or user pool logins |

---

## 🔎 IAM Whitepaper Insight

> “By default, all access is implicitly denied. Explicit **Allow** is required. However, any explicit **Deny** overrides **Allow**.”

---
# IAM User vs IAM Role in AWS

## IAM User
- Represents a person or application interacting directly with AWS
- Has **long-term credentials** (username/password, access keys)
- Belongs to IAM Groups
- Used for consistent long-term access

## IAM Role
- **No permanent credentials**
- Assumed by trusted entities (users, AWS services, applications)
- Grants **temporary credentials**
- Best for **delegating access** or **cross-account access**

### Differences Table

| Feature           | IAM User                            | IAM Role                                                  |
|------------------|-------------------------------------|-----------------------------------------------------------|
| Credentials       | Permanent (access keys)             | Temporary (via STS)                                       |
| Used By           | Human users, apps                   | IAM users, EC2, Lambda, other services                    |
| Credential Type   | Long-term                           | Short-term                                                |
| Trust Relationship| Not required                        | Required (defined in trust policy)                        |
| Use Case          | Direct AWS access                   | Delegated/temporary access                                |

---

# Restrict EC2 to Access Only One S3 Bucket

Use IAM **Role** with a **custom policy** and attach it to EC2.

## 1. IAM Policy

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": ["s3:GetObject", "s3:PutObject", "s3:ListBucket"],
      "Resource": [
        "arn:aws:s3:::your-single-s3-bucket-name",
        "arn:aws:s3:::your-single-s3-bucket-name/*"
      ]
    },
    {
      "Effect": "Deny",
      "Action": "s3:*",
      "Resource": "arn:aws:s3:::*",
      "Condition": {
        "StringNotLike": {
          "s3:prefix": [
            "your-single-s3-bucket-name/*",
            "your-single-s3-bucket-name"
          ]
        }
      }
    }
  ]
}

```
---
## IAM Role Trust Policy

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "ec2.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}

```
---
## Attach Role to EC2

During EC2 launch or via:  
**Actions → Security → Modify IAM Role**

---

## What is a Permissions Boundary?

A **Permissions Boundary** defines the **maximum permissions** an IAM role or user can have.

### Key Concepts

- Used to set **upper limits** on permissions  
- Helps with **delegated administration**  
- Evaluated in combination with **identity-based policies**

### How It Works

**Effective permissions = Intersection of:**

- Identity-based policy  
- Permissions boundary  

> If either denies access → **Access Denied**

### Example

You create a policy that:

- Allows only **EC2 and S3** actions  
- Denies **IAM** actions  

You then **attach it as a boundary** to all **developer roles**.

---

## IAM Policy: Restrict Access by IP Range

Use the `aws:SourceIp` condition key in IAM policies to restrict access from specific IP addresses.

### Example Policy

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": ["s3:GetObject", "s3:ListBucket", "ec2:DescribeInstances"],
      "Resource": "*",
      "Condition": {
        "IpAddress": {
          "aws:SourceIp": "203.0.113.0/24"
        }
      }
    },
    {
      "Effect": "Deny",
      "Action": "*",
      "Resource": "*",
      "Condition": {
        "NotIpAddress": {
          "aws:SourceIp": "203.0.113.0/24"
        },
        "BoolIfExists": {
          "aws:ViaAWSService": "false"
        }
      }
    }
  ]
}

```
## Explanation

- First statement allows actions from a specific IP range  
- Second statement denies any requests outside the IP range  
- `BoolIfExists` ensures AWS internal service calls are not blocked

---

## Federated Access from Active Directory to AWS

Use **IAM Identity Center (AWS SSO)** with **SAML 2.0 federation**.

### Integration Methods

- AD Connector (for on-prem AD)  
- AWS Managed Microsoft AD  
- External Identity Providers (IdPs) like ADFS

### Steps

1. Enable **IAM Identity Center**  
2. Connect to AD (via AD Connector or AWS Managed AD)  
3. Create Permission Sets (e.g., ReadOnly, Admin)  
4. Assign AD users/groups to AWS accounts and permission sets  
5. Access AWS via the **AWS SSO Portal**

---

### SAML Login Flow

1. User clicks AWS SSO login  
2. Redirect to on-prem AD/IdP (e.g., ADFS)  
3. Authenticates using AD credentials  
4. IdP issues SAML assertion to AWS  
5. AWS maps it to a role  
6. Temporary credentials are issued

---

### Benefits

- Centralized user management via AD  
- Use of temporary credentials (no long-term keys)  
- Full auditing via CloudTrail  
- Enforced MFA, strong password policies, etc.
