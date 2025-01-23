using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Entities;

public class TicketEntity
{
    public long Id { get; set; }
    public long SessionId { get; set; }
    public string? UserId { get; set; }
    public long MovieId { get; set; }
    public int Row {  get; set; }
    public UserEntity? User { get; set; }
    public SessionEntity? Session { get; set; }
    public MovieEntity? Movie { get; set; }
}
