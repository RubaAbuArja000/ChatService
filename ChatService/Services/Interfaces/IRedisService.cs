namespace ChatService.Services.Interfaces
{
    public interface IRedisService
    {
        Task PushToListAsync(string key, string value);
        Task<IEnumerable<string>> GetListRangeAsync(string key, int start, int stop);
        Task TrimListAsync(string key, int maxLength);
        Task IncrementAsync(string key);
        Task DecrementAsync(string key);
        Task<T> GetValueAsync<T>(string key);
        Task SetValueAsync(string key, string value);
        Task HashSetAsync(string hashKey, string field, string value);
        Task<Dictionary<string, string>> HashGetAllAsync(string hashKey);
    }
}