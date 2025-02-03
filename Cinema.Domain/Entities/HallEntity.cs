using System.Text.Json;

namespace Cinema.Domain.Entities;

public class HallEntity
{
    public int Id { get; set; }
    public string SeatsJson { get; set; }

    public List<List<int>> Seats
    {
        get => SeatsJson == null ? new List<List<int>>() : JsonSerializer.Deserialize<List<List<int>>>(SeatsJson);
        set => SeatsJson = JsonSerializer.Serialize(value);
    }
}
