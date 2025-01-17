using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Entities;

public class MovieEntity
{
    public int Id { get; set; }
    public int SearchId { get; set; }
    public double CinemaRating { get; set; }
}
