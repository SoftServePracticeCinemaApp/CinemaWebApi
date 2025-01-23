using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities;

public class SessionEntity
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public DateTime Date { get; set; }
    public int HallId { get; set; }
    public HallEntity? Hall { get; set; }
}
