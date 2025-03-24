using ChatService.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatService.Services
{
  
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _database;

        public RedisService(string connectionString)
        {
            _connection = ConnectionMultiplexer.Connect(connectionString);
            _database = _connection.GetDatabase();
        }

        public async Task PushToListAsync(string key, string value)
        {
            await _database.ListRightPushAsync(key, value);
        }

        public async Task<IEnumerable<string>> GetListRangeAsync(string key, int start, int stop)
        {
            var range = await _database.ListRangeAsync(key, start, stop);
            return range.Select(r => r.ToString());
        }

        public async Task TrimListAsync(string key, int maxLength)
        {
            await _database.ListTrimAsync(key, -maxLength, -1);
        }

        public async Task IncrementAsync(string key)
        {
            await _database.StringIncrementAsync(key);
        }

        public async Task DecrementAsync(string key)
        {
            await _database.StringDecrementAsync(key);
        }

        public async Task SetValueAsync(string key, string value)
        {
            // Using StringSetAsync to store the value
            bool isSet = await _database.StringSetAsync(key, value);
            if (!isSet)
            {
                throw new Exception($"Failed to set value for key: {key}");
            }
        }
        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
        public async Task HashSetAsync(string hashKey, string field, string value)
        {
            await _database.HashSetAsync(hashKey, field, value);
        }

        // Get all fields and values from a Redis hash
        public async Task<Dictionary<string, string>> HashGetAllAsync(string hashKey)
        {
            var hashEntries = await _database.HashGetAllAsync(hashKey);
            return hashEntries.ToDictionary(
                entry => entry.Name.ToString(),
                entry => entry.Value.ToString()
            );
        }
    }
}