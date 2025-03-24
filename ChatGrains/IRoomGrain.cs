namespace ChatGrains
{
    public interface IRoomGrain : IGrainWithIntegerKey
    {
        Task<List<string>> GetLastMessagesAsync();
        Task AddMessageAsync(string message);
    }
}
