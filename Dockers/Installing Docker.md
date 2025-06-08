# 🐳 Installing Docker

We'll cover installation on **Windows**, **Linux (Ubuntu)**, and **macOS**, and also explain **Docker Desktop vs Docker Engine**.

---

## 🧩 2.1 Docker Editions

### 🔹 Docker Engine (Community Edition - CE)
- Core Docker runtime.
- CLI + Daemon.
- Ideal for servers or headless setups (Linux mostly).

### 🔹 Docker Desktop
- GUI + Docker Engine + Docker Compose + Kubernetes (optional).
- Recommended for Windows and macOS developers.
- Handles OS-specific issues via virtualization (e.g., WSL2 on Windows).

---

## 💻 2.2 Installing Docker on Windows 10/11 (Recommended via Docker Desktop)

### ✅ Requirements
- Windows 10/11 Pro, Enterprise, or Education
- WSL2 enabled (Docker Desktop uses it for Linux containers)

### 🔧 Steps

1. **Install WSL2** (if not already installed):

```powershell
wsl --install
```

# 🐳 Installing Docker (Platform-wise Guide)

---

## 💻 2.2 Installing Docker on Windows 10/11

### 🔗 Download Docker Desktop
👉 [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop)

### 🔧 Installation Steps
1. Run the installer.
2. Enable **WSL2 backend** (default).
3. Let it install Ubuntu automatically if prompted.
4. After installation completes:
   - Launch **Docker Desktop**.
   - Wait until it shows **Docker is running**.

### ✅ Verify Installation (in PowerShell or CMD)
```bash
docker --version
docker info
```

## 🐧 2.3 Installing Docker on Ubuntu Linux (Server or Desktop)

```bash
# Update packages
sudo apt-get update

# Prerequisites
sudo apt-get install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release

# Add Docker GPG key
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | \
sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

# Add stable repository
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] \
  https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# Install Docker Engine
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io

# Post-install: Run docker as non-root
sudo usermod -aG docker $USER
newgrp docker

# Verify
docker --version
docker run hello-world
```

## 🧪 Post Installation Check

### Run:
```bash
docker run hello-world
```

### ✅ Expected Output:
```plaintext
Hello from Docker!
This message shows that your installation appears to be working correctly.
```

## 📎 Useful Notes

- **Docker Desktop vs Docker Engine**:
  - Use **Docker Desktop** for GUI, built-in Docker Compose/Kubernetes support, and simplified setup.
  - Use **Docker Engine** for Linux servers or cloud VM environments.

- You can **enable Kubernetes** from Docker Desktop settings.

- **Docker Compose** comes bundled with Docker Desktop.

## ✅ Summary

| OS            | Recommended Install            |
|---------------|--------------------------------|
| Windows 10/11 | Docker Desktop + WSL2          |
| Ubuntu        | Docker Engine (manual install) |
| macOS         | Docker Desktop                 |
