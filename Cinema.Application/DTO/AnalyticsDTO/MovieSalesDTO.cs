using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.AnalyticsDTO
{
    public class MovieSalesDTO
    {
        public string MovieTitle { get; set; }
        public int TicketsSold { get; set; }
        public double Revenue { get; set; }
    }
}
