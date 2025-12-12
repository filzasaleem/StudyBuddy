using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class ConnectionRequestDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
