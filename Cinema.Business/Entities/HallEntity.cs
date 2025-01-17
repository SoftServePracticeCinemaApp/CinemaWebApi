using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Entities;

public class HallEntity
{
    public int Id { get; set; }
    public List<List<int>> Seats { get; set; } = [];
}
