namespace Cinema.Domain.Entities;

public class SessionEntity
{
    public long Id { get; set; }
    public long MovieId { get; set; }
    public DateTime Date { get; set; }
    public int HallId { get; set; }
    public HallEntity? Hall { get; set; }
}
