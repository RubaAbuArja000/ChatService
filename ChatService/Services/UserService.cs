using ChatService.Services.Interfaces;

namespace ChatService.Services
{
    public class UserService : IUserService
    {
        private readonly IRedisService _redisService;

        public UserService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task IncrementLoginCountAsync(string userName)
        {
            var key = $"user:{userName}:logins";
            // Increment the login count in Redis
            await _redisService.IncrementAsync(key);
        }

        public async Task DecrementLoginCountAsync(string userName)
        {
            var key = $"user:{userName}:logins";
            // Increment the login count in Redis
            await _redisService.DecrementAsync(key);
        }

        public async Task<int> GetLoginCountAsync(string userName)
        {
            var key = $"user:{userName}:logins";
            // Retrieve the login count from Redis
            var count = await _redisService.GetValueAsync<int>(key);
            return count;
        }
    }
}