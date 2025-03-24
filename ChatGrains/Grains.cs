namespace ChatGrains
{
    public class RoomGrain : Grain, IRoomGrain
    {
        private List<string> _messages = new List<string>();

        public Task<List<string>> GetLastMessagesAsync()
        {
            return Task.FromResult(_messages.TakeLast(5).ToList());
        }

        public Task AddMessageAsync(string message)
        {
            _messages.Add(message);
            return Task.CompletedTask;
        }
    }

    public class UserGrain : Grain, IUserGrain
    {
        private int _loginCount = 0;
        private int _currentRoom = 0;

        public Task<int> GetLoginCountAsync()
        {
            return Task.FromResult(_loginCount);
        }

        public Task SetRoomAsync(int roomId)
        {
            _currentRoom = roomId;
            return Task.CompletedTask;
        }

        public Task<int> GetCurrentRoomAsync()
        {
            return Task.FromResult(_currentRoom);
        }

        public Task IncrementLoginCountAsync()
        {
            _loginCount++;
            return Task.CompletedTask;
        }
    }
}