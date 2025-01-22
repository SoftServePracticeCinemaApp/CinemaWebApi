using Cinema.Application.DTO.HallEntityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.SessionDTOs
{
    public class GetSessionDTO
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
        public GetHallDTO? Hall { get; set; }
    }
}
