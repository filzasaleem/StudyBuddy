using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Enums;

namespace Server.Models
{
    public class Connection
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public ConnectionStatus Status { get; set; } = ConnectionStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
