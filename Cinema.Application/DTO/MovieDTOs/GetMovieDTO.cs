using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.DTO.MovieDTOs
{
    public class GetMovieDTO
    {
        public int Id { get; set; }
        public int SearchId { get; set; }
        public double CinemaRating { get; set; }
    }
}
