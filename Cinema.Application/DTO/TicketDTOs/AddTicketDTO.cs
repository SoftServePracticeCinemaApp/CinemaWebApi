using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.TicketDTOs
{
    public class AddTicketDTO
    {
        public long SessionId { get; set; }
        public string? UserId { get; set; }
        public long MovieId { get; set; }
        public int Row { get; set; }
    }
}
