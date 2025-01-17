using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Entities;

public class UserEntity
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public List<TicketEntity> Tickets { get; set; } = [];
}
