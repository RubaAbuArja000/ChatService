using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private readonly IClusterClient _orleansClient;
    private readonly IUserService _userService;
    private readonly IRoomService _roomService;

    public ChatHub(IClusterClient orleansClient, IUserService userService, IRoomService roomService)
    {
        _orleansClient = orleansClient;
        _userService = userService;
        _roomService = roomService;
    }
    public override async Task OnConnectedAsync()
    {
        var roomId = Context.GetHttpContext().Request.Query["roomId"].ToString();
        var userName = Context.GetHttpContext().Session.GetString("Username");

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.OthersInGroup(roomId.ToString()).SendAsync("UserConnected", userName);

        await _userService.IncrementLoginCountAsync(userName);
        await _roomService.IncrementLoginCountAsync(int.Parse(roomId));

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var roomId = Context.GetHttpContext().Request.Query["roomId"].ToString();
        var userName = Context.GetHttpContext().Session.GetString("Username");

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.OthersInGroup(roomId.ToString()).SendAsync("UserDisconnected", userName);

        await _userService.DecrementLoginCountAsync(userName);
        await _roomService.DecrementLoginCountAsync(int.Parse(roomId));

        await base.OnDisconnectedAsync(exception);
    }
}
