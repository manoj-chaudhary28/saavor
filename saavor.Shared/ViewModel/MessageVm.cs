using System;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class MessageVm
    {
        public string MessageId { get; set; }
        public Int64 ProfileId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CreateDate { get; set; }        
    }

    public class MessageModel
    {
        [Key]
        public Int64 MessageId { get; set; }
        public Int64 ProfileId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CreateDate { get; set; }
        public Int32 TotalRecord { get; set; }
    }

    public class MessageResponseVm
    {
        public string DeviceType { get; set; }
        [Key]
        public string DeviceToken { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
         
    }
}
