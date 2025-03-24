using ChatService.Dtos;

namespace ChatService.Services.Interfaces
{
    public interface IRoomService
    {
        Task CreateRoomAsync(int roomId, string roomName);
        Task<List<MessageDto>> GetMessagesAsync(int roomId);
        Task SendMessageAsync(int roomId, MessageDto message);
        Task<List<RoomDto>> GetAllRoomsAsync();
        Task IncrementLoginCountAsync(int roomId);
        Task DecrementLoginCountAsync(int roomId);
        Task<int> GetLoginCountAsync(int roomId);
    }
}