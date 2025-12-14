using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class NotificationDto
    {
        public Guid ConnectionId { get; set; }
        public Guid SenderId { get; set; }
        public string? SenderFirstName { get; set; }
        public string? SenderLastName { get; set; }
        public string? SenderEmail { get; set; }
    }
}
