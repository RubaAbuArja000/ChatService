using ChatService.Dtos;
using ChatService.Services.Interfaces;
using Newtonsoft.Json;

namespace ChatService.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRedisService _redisService;

        public RoomService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<List<MessageDto>> GetMessagesAsync(int roomId)
        {
            var messages = await _redisService.GetListRangeAsync($"room:{roomId}:messages", -5, -1); // Last 4 messages
            if (messages.Any())
                return messages.Select(msg => JsonConvert.DeserializeObject<MessageDto>(msg)).ToList();
            return [];
        }

        public async Task SendMessageAsync(int roomId, MessageDto message)
        {
            string messageJson = JsonConvert.SerializeObject(message);
            await _redisService.PushToListAsync($"room:{roomId}:messages", messageJson);
        }


        public async Task CreateRoomAsync(int roomId, string roomName)
        {
            var roomKey = "rooms";

            await _redisService.HashSetAsync(roomKey, roomId.ToString(), roomName);
        }

        // Get all rooms from the Redis hash
        public async Task<List<RoomDto>> GetAllRoomsAsync()
        {
            var roomKey = "rooms";

            var roomData = await _redisService.HashGetAllAsync(roomKey);
            var roomDtos = new List<RoomDto>();

            foreach (var s in roomData)
            {
                var roomDto = new RoomDto
                {
                    RoomId = int.Parse(s.Key),
                    RoomName = s.Value,
                    LoginCount = await GetLoginCountAsync(int.Parse(s.Key)),
                };

                roomDtos.Add(roomDto);
            }

            return roomDtos;
        }

        public async Task IncrementLoginCountAsync(int roomId)
        {
            var key = $"room:{roomId}:logins";
            // Increment the login count in Redis
            await _redisService.IncrementAsync(key);
        }

        public async Task DecrementLoginCountAsync(int roomId)
        {
            var key = $"room:{roomId}:logins";
            // Increment the login count in Redis
            await _redisService.DecrementAsync(key);
        }

        public async Task<int> GetLoginCountAsync(int roomId)
        {
            var key = $"room:{roomId}:logins";
            // Retrieve the login count from Redis
            var count = await _redisService.GetValueAsync<int>(key);
            return count;
        }
    }
}