# ChatService
# Chat Service with Redis, SignalR, and Orleans

A simple chat service built using ASP.NET Core, SignalR, Microsoft Orleans, and Redis for real-time messaging. Users can join chat rooms, send messages, and track their login history.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (6.0+)
- [Redis](https://redis.io/download)
- [Visual Studio Code](https://code.visualstudio.com/) or any IDE

## Setup

1. **Install Redis** and ensure it is running on `localhost:6379`.
2. Clone the repository:

   ```bash
   git clone https://github.com/YOUR_USERNAME/your-repository.git
   cd your-repository
Build and Run the application:

## Features

### SignalR Chat:
- **Change Room**: Switch rooms and retrieve the last 5 messages.
- **Send Message**: Send messages to users in the same room.

### Orleans Grains:
- **Room Grain**: Stores messages per room.
- **User Grain**: Tracks user logins and room choices.

### Redis
- Stores messages and user data (logins, room preferences).

## Usage

1. **Start the server**:

   To start the server, run the following command in your terminal:

   ```bash
   dotnet run
