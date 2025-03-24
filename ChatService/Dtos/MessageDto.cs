namespace ChatService.Dtos
{
    public class MessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
    }
}