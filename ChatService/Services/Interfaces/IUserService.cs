namespace ChatService.Services.Interfaces
{
    public interface IUserService
    {
        Task IncrementLoginCountAsync(string userName);
        Task DecrementLoginCountAsync(string userName);
        Task<int> GetLoginCountAsync(string userName);
    }
}