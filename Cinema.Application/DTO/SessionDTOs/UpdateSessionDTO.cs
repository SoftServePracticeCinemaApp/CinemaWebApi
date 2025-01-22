using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.SessionDTOs
{
    public class UpdateSessionDTO
    {
        public long MovieId { get; set; }
        public DateTime Date { get; set; }
        public int HallId { get; set; }
    }
}
