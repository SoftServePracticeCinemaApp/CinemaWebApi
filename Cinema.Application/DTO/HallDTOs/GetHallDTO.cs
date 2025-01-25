using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.HallEntityDTOs
{
    public class GetHallDTO
    {
        public int Id { get; set; }
        public List<List<int>> Seats { get; set; }
    }
}
