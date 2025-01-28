using Microsoft.AspNetCore.Identity;

namespace Cinema.Domain.Entities;

public class UserEntity : IdentityUser
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public IEnumerable<TicketEntity> Tickets { get; set; } = [];
}
