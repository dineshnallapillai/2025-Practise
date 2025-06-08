
# Docker CLI & Basic Commands

In this section, you'll learn how to interact with Docker using the command line, run containers, view logs, manage running containers, and clean up resources.

## 🛠️ 3.1 Checking Docker Setup

```bash
docker --version      # Show Docker version
docker info           # System-wide Docker details
```

## ▶️ 3.2 Running Your First Container

```bash
docker run hello-world
```

**What this does:**

- Pulls the `hello-world` image (if not already present)
- Creates a container
- Runs the app (displays a success message)
- Exits automatically

## 🚀 3.3 Run a Long-Running Container

```bash
docker run -d -p 8080:80 nginx
```

- `-d` = detached mode (runs in background)
- `-p` = publish container's port 80 to host port 8080
- `nginx` = official lightweight web server image

📌 Open browser at `http://localhost:8080` to see it in action.

## 🔍 3.4 View Running Containers

```bash
docker ps
```

To show all containers (running and stopped):

```bash
docker ps -a
```

## 🧹 3.5 Stopping and Removing Containers

**Stop by Container ID or Name:**

```bash
docker stop <container-id>
```

**Remove a container:**

```bash
docker rm <container-id>
```

**Stop and Remove in one line:**

```bash
docker rm -f <container-id>
```

## 🔧 3.6 Executing Commands Inside a Running Container

```bash
docker exec -it <container-id> bash
```

- `-it` = interactive terminal
- `bash` or `sh` (depending on image)

**Example:**

```bash
docker exec -it my-nginx-container bash
```

## 📜 3.7 Viewing Container Logs

```bash
docker logs <container-id>
```

To stream logs in real-time:

```bash
docker logs -f <container-id>
```

## 🧊 3.8 Viewing and Managing Images

```bash
docker images        # List all images
docker rmi <image>   # Remove an image
```

## 🧼 3.9 Cleaning Up Docker Resources

**Remove all stopped containers:**

```bash
docker container prune
```

**Remove all unused images:**

```bash
docker image prune
```

**Remove everything not in use:**

```bash
docker system prune
```

**Add -a to remove all unused volumes, networks, and images:**

```bash
docker system prune -a
```

## 🔁 3.10 Repeating a Command in One Shot

Run and delete the container after use:

```bash
docker run --rm alpine echo "Hello from Alpine!"
```

## 🔖 Common Flags Summary

| Flag     | Meaning                             |
|----------|-------------------------------------|
| `-d`     | Detached (background) mode          |
| `-p`     | Port mapping (host:container)       |
| `-it`    | Interactive terminal                |
| `--rm`   | Auto-remove container after run     |
| `--name` | Assign name to container            |

## ✅ Summary

| Task                 | Command Example                    |
|----------------------|------------------------------------|
| Run container        | `docker run nginx`                 |
| Detached run + port  | `docker run -d -p 8080:80 nginx`   |
| List running         | `docker ps`                        |
| List all             | `docker ps -a`                     |
| Stop container       | `docker stop <id>`                 |
| Remove container     | `docker rm <id>`                   |
| Logs                 | `docker logs <id>`                 |
| Interactive shell    | `docker exec -it <id> bash`        |
| Clean up system      | `docker system prune`              |


