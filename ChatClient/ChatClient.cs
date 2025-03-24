using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub") 
                .Build();

            connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"Received: {message}");
            });

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connected to the chat service.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect: {ex.Message}");
                return;
            }

            Console.WriteLine("Enter your username:");
            var username = Console.ReadLine();

            while (true)
            {
                var input = Console.ReadLine();
                if (input.StartsWith("Room"))
                {
                    if (int.TryParse(input.Split(' ')[1], out int roomId))
                    {
                        await connection.InvokeAsync("ChangeRoom", roomId); 
                        Console.WriteLine($"Changed to room {roomId}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid room number.");
                    }
                }
                else if (input.StartsWith("Send"))
                {
                    var message = input.Substring(5);
                    await connection.InvokeAsync("SendMessage", message); 
                }
                else
                {
                    Console.WriteLine("Invalid command. Use 'Room [number]' or 'Send [message]'.");
                }
            }
        }
    }
}