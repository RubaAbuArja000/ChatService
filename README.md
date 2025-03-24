# ChatService
# Chat Service with Redis, SignalR, and Orleans

A simple chat service built using ASP.NET Core, SignalR, Microsoft Orleans, and Redis for real-time messaging. Users can join chat rooms, send messages, and track their login history.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (6.0+)
- [Redis](https://redis.io/download)
- [Visual Studio Code](https://code.visualstudio.com/) or any IDE
- [Node.js](https://nodejs.org/)

## Setup

1. **Install Redis** and ensure it is running on `localhost:6379`.
2. Clone the repository:

   ```bash
   git clone https://github.com/YOUR_USERNAME/your-repository.git
   cd your-repository
Build and Run the application:

bash
Copy
dotnet restore
dotnet build
dotnet run
Ensure Redis is running by checking:

bash
Copy
redis-cli ping
Features
SignalR Chat:

Change Room: Switch rooms and retrieve last 5 messages.

Send Message: Send messages to users in the same room.

Orleans Grains:

Room Grain: Stores messages per room.

User Grain: Tracks user logins and room choices.

Redis stores messages and user data (logins, room preferences).

Usage
Start the server:

bash
Copy
dotnet run
Open the test client in your browser (e.g., http://localhost:5000).

Use the following commands in the client:

Room [room number]: Join a room.

Send [message]: Send a message.

Deployment
To deploy, consider using Docker:

bash
Copy
docker build -t chat-service .
docker run -p 5000:5000 chat-service
