using Cinema.BlazorUI.Model;
using System.Text.Json.Serialization;
using System.Text.Json;

public class FormattedHall
{
    private int _id;
    private string _seatsJson;

    [JsonPropertyName("id")]
    public int Id
    {
        get => _id;
        set
        {
            Console.WriteLine($"Setting Id to: {value}");
            _id = value;
        }
    }

    [JsonIgnore]
    public string SeatsJson
    {
        get => _seatsJson;
        set
        {
            Console.WriteLine($"Setting SeatsJson to: {value}"); 
            _seatsJson = value;
        }
    }

    public List<List<int>> Seats
    {
        get => SeatsJson == null ? new List<List<int>>() : JsonSerializer.Deserialize<List<List<int>>>(SeatsJson);
        set => SeatsJson = JsonSerializer.Serialize(value);
    }

    public ICollection<FormattedSession> Sessions { get; set; }
}