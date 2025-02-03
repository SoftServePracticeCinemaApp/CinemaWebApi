namespace Cinema.Domain.Entities;

public class SessionEntity
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public DateTime Date { get; set; }
    public int HallId { get; set; }
    public HallEntity? Hall { get; set; }

    public MovieEntity? Movie { get; set; }
}
