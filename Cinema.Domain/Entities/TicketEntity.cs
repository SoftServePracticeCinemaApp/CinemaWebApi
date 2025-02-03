namespace Cinema.Domain.Entities;

public class TicketEntity
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public string? UserId { get; set; }
    public int MovieId { get; set; }
    public int Row {  get; set; }
    public UserEntity? User { get; set; }
    public SessionEntity? Session { get; set; }
    public MovieEntity? Movie { get; set; }

}
