using ChatService.Dtos;
using ChatService.Services;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> _hubContext;

        public RoomsController(IRoomService? roomService, IUserService? userService, IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            _roomService = roomService;
            _hubContext = hubContext;
        }

        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            HttpContext.Session.SetString("Username", dto.Username);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var roomId = new Random().Next(1, 10000);
            await _roomService.CreateRoomAsync(roomId, dto.RoomName);
            return Ok(new RoomDto { RoomId = roomId, RoomName = dto.RoomName });
        }

        [HttpGet("{roomId}/messages")]
        public async Task<IActionResult> GetMessages(int roomId)
        {
            string username = HttpContext.Session.GetString("Username");

            var messages = await _roomService.GetMessagesAsync(roomId);
            var data = new List<object>();

            foreach (var msg in messages)
            {
                var loginCount = await _userService.GetLoginCountAsync(msg.Sender);

                var messageData = new
                {
                    msg.Message,
                    msg.Sender,
                    msg.CreationDate,
                    isFromMe = msg.Sender == username,
                    LoginCount = loginCount
                };

                data.Add(messageData);
            }

            return Ok(data);
        }

        [HttpPost("{roomId}/messages")]
        public async Task<IActionResult> SendMessage(int roomId, [FromBody] SendMessageDto dto)
        {
            string username = HttpContext.Session.GetString("Username");

            MessageDto messageDto = new MessageDto()
            {
                CreationDate = DateTime.UtcNow,
                Message = dto.Message,
                Sender = username
            };

            await _roomService.SendMessageAsync(roomId, messageDto);
            await _hubContext.Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", messageDto);
            return Ok(new { status = "Message sent successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }
    }
}
