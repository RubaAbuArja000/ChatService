namespace ChatGrains
{
    public interface IUserGrain : IGrainWithStringKey
    {
        Task<int> GetLoginCountAsync();
        Task SetRoomAsync(int roomId);
        Task<int> GetCurrentRoomAsync();
        Task IncrementLoginCountAsync();
    }
}
