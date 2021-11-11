using System;
 
namespace saavor.Shared.DTO.Message
{
    public class MessageDTO
    {
        public Int64 MessageId { get; set; }
        public Int64 ProfileId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string Search { get; set; }
    }
}
