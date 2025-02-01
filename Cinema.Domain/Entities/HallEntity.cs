namespace Cinema.Domain.Entities;

public class HallEntity
{
    public int Id { get; set; }
    public List<List<int>> Seats { get; set; } = [];
}
